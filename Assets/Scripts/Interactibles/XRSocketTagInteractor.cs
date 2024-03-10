using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketTagInteractor : XRSocketInteractor
{
    public string targetTag;
    private HoleController holeController;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.CompareTag(targetTag);
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && interactable.transform.CompareTag(targetTag);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        WedgeController wedge = args.interactableObject.transform.GetComponentInChildren<WedgeController>();
        if (wedge != null)
        {
            Debug.Log("wedge in place");
            wedge.SetCurrentSocket(this);
        }
    }

    public void InitRefToController(HoleController hole)
    {
        holeController = hole;
    }

    public void OnHammerHit()
    {
        holeController.OnHammerHit();
    }
}
