using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ConstraintGrabInteractable : XRGrabInteractable
{
    private Rigidbody rigidBody; // Reference to the Rigidbody component

    protected override void Awake()
    {
        base.Awake();
        // Initialize the Rigidbody reference
        rigidBody = GetComponent<Rigidbody>();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        // Apply constraints to the Rigidbody to only allow movement along the Z-axis
        if (rigidBody != null && args.interactorObject is XRDirectInteractor)
        {
            rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        // Remove the constraints from the Rigidbody when the interaction ends
        if (rigidBody != null)
        {
            rigidBody.constraints = RigidbodyConstraints.None;
        }
    }
}