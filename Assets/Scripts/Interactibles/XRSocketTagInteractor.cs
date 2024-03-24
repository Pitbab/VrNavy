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
        // if the socket row is not initialized then any wedge can hover on the socket
        if (!isInitialized)
        {
            return base.CanHover(interactable) && interactable.transform.CompareTag(targetTag);
        }
        
        //else it needs to be the same game object the row was initialized with
        return base.CanHover(interactable) && interactable.transform.CompareTag(targetTag) && trackId == interactable.transform.GetInstanceID();
    }
    
    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        // if the wedge is not already on a track then it can be selected on on this one 
        WedgeController wedgeController = interactable.transform.GetComponentInChildren<WedgeController>();
        if (wedgeController != null)
        {
            if (!wedgeController.isInTrack)
            {
                return base.CanSelect(interactable) && interactable.transform.CompareTag(targetTag);
            }
        }
        
        //else it needs to be the same game object the row was initialized with
        return base.CanSelect(interactable) && interactable.transform.CompareTag(targetTag) && trackId == interactable.transform.GetInstanceID();
        
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        WedgeController wedge = args.interactableObject.transform.GetComponentInChildren<WedgeController>();
        if (wedge != null)
        {
            //get the socket ref for the wedge
            Debug.Log("wedge in place");
            wedge.SetCurrentSocket(this);
            
            //if the track wasn't initialized then set the wedge state to "in track" and get its objectID
            if (!isInitialized)
            {
                wedge.isInTrack = true;
                SetSocketId(wedge.transform.parent.GetInstanceID());
            }

        }
    }

    //notifies all sockets within the row that only the object with the specified ID is considered valid
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
