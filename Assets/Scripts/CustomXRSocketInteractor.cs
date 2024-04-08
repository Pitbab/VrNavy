using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomXRSocketInteractor : XRSocketInteractor
{
    public Vector3 maxSize = new Vector3(1f, 3f, 1f); // Maximum allowed size
    public Vector3 minSize = new Vector3(0.1f, 0.5f, 0.1f); // Minimum allowed size
    public string firstChildNameToCheck = "FirstTargetChild"; // Name of the first child to check
    public string secondChildNameToCheck = "SecondTargetChild"; // Name of the second child to check

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

    // Checks if the specified child's collider size is within defined limits
    bool IsWithinSizeLimits(GameObject objectToCheck)
    {
        Transform childTransform = objectToCheck.transform.Find(firstChildNameToCheck);
        Debug.Log(childTransform.name);
        if (childTransform == null)
        {
            Debug.Log(childTransform.name);
            // If the first child isn't found, try finding the second child
            childTransform = objectToCheck.transform.Find(secondChildNameToCheck);
        }

        if (childTransform != null)
        {
            Debug.Log("Entered if childTransform != null");
            Collider childCollider = childTransform.GetComponent<Collider>();
            if (childCollider != null)
            {
                Vector3 objectSize = childCollider.bounds.size;
                Debug.Log("Collider Size: " + childCollider.bounds.size);

                // Check if the object size is within the specified min and max size
                bool widthCheck = objectSize.x >= minSize.x && objectSize.x <= maxSize.x;
                bool heightCheck = objectSize.y >= minSize.y && objectSize.y <= maxSize.y;
                bool depthCheck = objectSize.z >= minSize.z && objectSize.z <= maxSize.z;

                return widthCheck && heightCheck && depthCheck;
            }
        }
        return false; // No suitable child found or no collider on the child
    }
}
