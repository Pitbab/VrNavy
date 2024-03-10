using System;
using System.Collections;
using System.Collections.Generic;
using EzySlice;
using UnityEngine;
using Plane = EzySlice.Plane;
using Random = UnityEngine.Random;

public class SliceTesting : MonoBehaviour
{
    public Material crossMaterial;
    public float separationSpeed = 15f;

    public void SlideObject(GameObject obj)
    {
        Vector3 pos = gameObject.GetComponent<Collider>().bounds.center;
        Vector3 dir = Vector3.down;

        Plane p = new Plane(pos, dir);
        GameObject[] slices = obj.SliceInstantiate(p, new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f), crossMaterial);

        if (slices != null && slices.Length > 0)
        {

            // add rigidbodies and colliders
            foreach (GameObject shatteredObject in slices)
            {
                shatteredObject.AddComponent<MeshCollider>().convex = true;
                shatteredObject.AddComponent<Rigidbody>();

            }
        }
        
        //Destroy(gameObject);
    }
    
    public bool ExShatterObject(GameObject obj, int iterations, Material crossSectionMaterial = null) {
        if (iterations > 0) {
            GameObject[] slices = obj.SliceInstantiate(GetRandomPlane(obj.transform.position, obj.transform.localScale),
                new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
                crossSectionMaterial);

            if (slices != null) {
                // shatter the shattered!
                for (int i = 0; i < slices.Length; i++) {
                    if (ExShatterObject(slices[i], iterations - 1, crossSectionMaterial)) {
                        // delete the parent
                        GameObject.DestroyImmediate(slices[i]);
                    }
                }

                return true;
            }

            return ExShatterObject(obj, iterations - 1, crossSectionMaterial);
        }

        return false;
    }
    
    public void ShatterObject(Vector3 colPos, Vector3 normal) 
    {
        Plane p = GetRandomPlane(gameObject.transform.position, gameObject.transform.localScale);
        //Plane p = new Plane(colPos, normal);
        GameObject[] slices = gameObject.SliceInstantiate(p, new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f), crossMaterial);

        if (slices != null)
        {
            for (int i = 0; i < slices.Length; i++)
            {
                slices[i].AddComponent<MeshCollider>().convex = true;
                slices[i].AddComponent<Rigidbody>().AddForce(p.normal * separationSpeed, ForceMode.Impulse);
                slices[i].AddComponent<SliceTesting>().crossMaterial = crossMaterial;
            }
                
            Destroy(gameObject);
        }
    }

    /**
     * Given an offset position and an offset scale, calculate a random plane
     * which can be used to randomly slice an object
     */
    public EzySlice.Plane GetRandomPlane(Vector3 positionOffset, Vector3 scaleOffset) {
        Vector3 randomPosition = Random.insideUnitSphere;

        randomPosition += positionOffset;

        Vector3 randomDirection = Random.insideUnitSphere.normalized;

        return new EzySlice.Plane(randomPosition, randomDirection);
    }
}
