using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicRig : MonoBehaviour
{
    public Transform playerHead, leftController, rightController;
    public CapsuleCollider bodyCollider;

    public ConfigurableJoint headJoint, leftHandJoint, rightHandJoint;

    private float bodyHeightMin = 0.5f;
    private float bodyHeightMax = 2f;

    private void FixedUpdate()
    {
        bodyCollider.height = Mathf.Clamp(playerHead.localPosition.y, bodyHeightMin, bodyHeightMax);
        bodyCollider.center =
            new Vector3(playerHead.localPosition.x, bodyCollider.height / 2, playerHead.localPosition.z);

        leftHandJoint.targetPosition = leftController.localPosition;
        leftHandJoint.targetRotation = leftController.localRotation;
        
        rightHandJoint.targetPosition = rightController.localPosition;
        rightHandJoint.targetRotation = rightController.localRotation;
        
        headJoint.targetPosition = playerHead.localPosition;
        headJoint.targetRotation = playerHead.localRotation;
    }
}
