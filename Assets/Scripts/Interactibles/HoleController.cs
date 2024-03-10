using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    [SerializeField] private List<XRSocketTagInteractor> sockets;
    private int currentSocketIndex;
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

    public void OnHammerHit()
    {
        if ((currentSocketIndex + 1) < sockets.Count)
        {
            sockets[currentSocketIndex].enabled = false;
            currentSocketIndex++;
            sockets[currentSocketIndex].enabled = true;
        }
        
    }
    
}
