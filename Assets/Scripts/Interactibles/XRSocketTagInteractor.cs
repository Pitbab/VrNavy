using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketTagInteractor : XRSocketInteractor
{
    public string targetTag;
    private HoleController holeController;
    public bool isInitialized = false;
    public int trackId;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        if (!isInitialized)
        {
            return base.CanHover(interactable) && interactable.transform.CompareTag(targetTag);
        }
        
        return base.CanHover(interactable) && interactable.transform.CompareTag(targetTag) && trackId == interactable.transform.GetInstanceID();
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        WedgeController wedgeController = interactable.transform.GetComponentInChildren<WedgeController>();
        if (wedgeController != null)
        {
            if (!wedgeController.isInTrack)
            {
                return base.CanSelect(interactable) && interactable.transform.CompareTag(targetTag);
            }
        }
        
        return base.CanSelect(interactable) && interactable.transform.CompareTag(targetTag) && trackId == interactable.transform.GetInstanceID();
        
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        Debug.Log("exited");
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        WedgeController wedge = args.interactableObject.transform.GetComponentInChildren<WedgeController>();
        if (wedge != null)
        {
            Debug.Log("wedge in place");
            wedge.SetCurrentSocket(this);
            
            if (!isInitialized)
            {
                wedge.isInTrack = true;
                SetSocketId(wedge.transform.parent.GetInstanceID());
            }

        }
    }

    private void SetSocketId(int id)
    {
        foreach (var socket in holeController.sockets)
        {
            socket.trackId = id;
            socket.isInitialized = true;
        }
    }
    
    public void InitRefToController(HoleController hole)
    {
        holeController = hole;
    }

    public void OnHammerHit(XRBaseInteractable interactable)
    {
        holeController.OnHammerHit(interactable);
    }
}
