using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabPhysics : MonoBehaviour
{
    public InputActionProperty grabInputSource;
    public float radius = 0.1f;
    public LayerMask grabLayer;

    private FixedJoint fixedJoint;
    private bool isGrabbing = false;

    private GrabbableItem itemInHand;
    
    private void FixedUpdate()
    {
        bool isGrabButtonPressed = grabInputSource.action.ReadValue<float>() > 0.3f;

        if (isGrabButtonPressed && !isGrabbing)
        {
            Collider[] nearbyCollider =
                Physics.OverlapSphere(transform.position, radius, grabLayer, QueryTriggerInteraction.Ignore);

            if (nearbyCollider.Length <= 0) return;
            
            Rigidbody nearbyRigidBody = nearbyCollider[0].attachedRigidbody;
            fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.autoConfigureConnectedAnchor = false;

            if (nearbyRigidBody)
            {
                fixedJoint.connectedBody = nearbyRigidBody;
                fixedJoint.connectedAnchor = nearbyRigidBody.transform.InverseTransformPoint(transform.position);
            }
            else
            {
                fixedJoint.connectedAnchor = transform.position;
            }

            GrabbableItem i = nearbyCollider[0].gameObject.GetComponent<GrabbableItem>();

            if (i != null)
            {
                itemInHand = i;
                itemInHand.OnGrab();
            }

            isGrabbing = true;
        }
        else if(!isGrabButtonPressed && isGrabbing)
        {
            if (itemInHand != null)
            {
                itemInHand.OnRelease();
                itemInHand = null;
            }

            isGrabbing = false;
            if (fixedJoint)
            {
                Destroy(fixedJoint);
            }
            
        }
    }
}
