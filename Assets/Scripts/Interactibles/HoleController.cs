using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoleController : MonoBehaviour
{
    public List<XRSocketTagInteractor> sockets = new List<XRSocketTagInteractor>();
    private int currentSocketIndex;
    private int gameObjectId;
    private void Start()
    {
        //deactivate all socket except the first one
        foreach (var socket in sockets)
        {
            socket.InitRefToController(this);
            socket.enabled = false;
        }

        currentSocketIndex = 0;
        sockets[currentSocketIndex].enabled = true;
    }

    public void OnHammerHit(XRBaseInteractable interactable)
    {
        
        //if the wedge is not a the max index we can hammer it further
        if ((currentSocketIndex + 1) < sockets.Count)
        {
            sockets[currentSocketIndex].enabled = false;
            currentSocketIndex++;
            sockets[currentSocketIndex].enabled = true;
        }
        //else we disable the interaction with the wedge
        else if(currentSocketIndex + 1 == sockets.Count)
        {
            interactable.enabled = false;
            Rigidbody rb = interactable.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.freezeRotation = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

    }
    
}
