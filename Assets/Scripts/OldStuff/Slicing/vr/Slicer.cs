using System;
using System.Collections;
using UnityEngine;
using EzySlice;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class Slicer : MonoBehaviour
{
    public Material materialAfterSlice;
    public LayerMask sliceMask;
    public bool isTouched;
    [SerializeField] private float coolDown = 0.1f;
    private float timer = 0f;

    private void Start()
    {
        timer = coolDown;
    }

    //probably better to do this with trigger enter and exit 
    private void Update()
    {

        if (timer < coolDown)
        {
            timer += Time.deltaTime;
            return;
        }

        if (isTouched)
        {
            timer = 0f;
            isTouched = false;
            
            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);
            
            foreach (Collider objectToBeSliced in objectsToBeSliced)
            {
                SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, materialAfterSlice);
                
                if(slicedObject == null) return;
                
                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, materialAfterSlice);
                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, materialAfterSlice);

                upperHullGameobject.transform.position = objectToBeSliced.transform.position;
                lowerHullGameobject.transform.position = objectToBeSliced.transform.position;

                MakeItPhysical(upperHullGameobject);
                MakeItPhysical(lowerHullGameobject);

                Destroy(objectToBeSliced.gameObject);
            }
        }
    }

    private void MakeItPhysical(GameObject obj)
    {

        MeshCollider mesh = obj.AddComponent<MeshCollider>();
        mesh.convex = true;

        GameObject parent = new GameObject();
        parent.transform.position = mesh.bounds.center;

        obj.transform.SetParent(parent.transform);
        Rigidbody rb = parent.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        XRGrabInteractable grab = parent.AddComponent<XRGrabInteractable>();
        grab.movementType = XRBaseInteractable.MovementType.Kinematic;
        
        //need to find a way to make it sliceable with interaction layer
        
        //obj.layer = LayerMask.NameToLayer("Sliceable");
        //grab.interactionLayers = InteractionLayerMask.NameToLayer("Sliceable");
        //parent.layer = LayerMask.NameToLayer("Sliceable");
        //grab.attachTransform = obj.transform;
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }
    


}
