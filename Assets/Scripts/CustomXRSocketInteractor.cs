using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomXRSocketInteractor : XRSocketInteractor
{
    public Vector3 maxSize = new Vector3(1f, 3f, 1f); // Maximum allowed size
    public Vector3 minSize = new Vector3(0.1f, 0.5f, 0.1f); // Minimum allowed size

    // Override the method used to validate objects for socketing
    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        // First, check if the base conditions are met
        if (!base.CanSelect(interactable))
            return false;

        // Convert IXRSelectInteractable to a GameObject
        GameObject interactableObject = (interactable as Component)?.gameObject;

        // If conversion is successful, perform the custom size check
        if (interactableObject != null)
        {
            return IsWithinSizeLimits(interactableObject);
        }

        // If conversion fails, return false
        return false;
    }

    // Checks if the interactable object's size is within defined limits
    bool IsWithinSizeLimits(GameObject objectToCheck)
    {
        Collider objectCollider = objectToCheck.GetComponent<Collider>();
        if (objectCollider != null)
        {
            Vector3 objectSize = objectCollider.bounds.size;

            // Check if the object size is within the specified min and max size
            bool widthCheck = objectSize.x >= minSize.x && objectSize.x <= maxSize.x;
            bool heightCheck = objectSize.y >= minSize.y && objectSize.y <= maxSize.y;
            bool depthCheck = objectSize.z >= minSize.z && objectSize.z <= maxSize.z;

            return widthCheck && heightCheck && depthCheck;
        }
        return false;
    }
}

