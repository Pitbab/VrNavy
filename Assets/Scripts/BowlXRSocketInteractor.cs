using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowlXRSocketInteractor : XRSocketInteractor
{
    public string requiredTag = "Bowl";

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        // Convert IXRSelectInteractable back to XRBaseInteractable to access Unity-specific functions like CompareTag
        XRBaseInteractable baseInteractable = interactable as XRBaseInteractable;

        // Ensure the interactable is not null and check the tag
        if (baseInteractable != null)
        {
            return baseInteractable.CompareTag(requiredTag) && base.CanSelect(interactable);
        }

        return false;
    }
}