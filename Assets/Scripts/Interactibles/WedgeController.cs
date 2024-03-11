using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WedgeController : MonoBehaviour
{
    public string targetTag;
    private bool isHitting = false;
    private XRSocketTagInteractor currentSocket;
    [SerializeField] private AudioClip hammeringSound;
    private AudioSource audioSource;
    public bool isInTrack = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetCurrentSocket(XRSocketTagInteractor socket)
    {
        currentSocket = socket;
    }
    private void OnTriggerEnter(Collider other)
    {
        //if not in a socket
        if(currentSocket == null) return;
        //if not hitting
        if (isHitting) return;
        //if is the hammer head
        if (other.CompareTag(targetTag))
        {
            Debug.Log("hammer hit detected");
            //get the interactable
            XRGrabInteractable interactable = GetComponentInParent<XRGrabInteractable>();
            if (interactable != null)
            {
                currentSocket.OnHammerHit(interactable);
                isHitting = true;
                audioSource.PlayOneShot(hammeringSound);
            }
        }
    }

    //initiates a cooldown for hitting if the hammer is no longer in contact with the collider Once the cooldown ends, hitting will become possible again.
    private void OnTriggerExit(Collider other)
    {
        if (isHitting)
        {
            if (other.CompareTag(targetTag))
            {
                Invoke(nameof(HittingCooldown), 0.2f);
            }
        }
    }

    //use to not spam the OnHitHammer function 
    private void HittingCooldown()
    {
        isHitting = false;
    }
}

