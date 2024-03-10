using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WedgeController : MonoBehaviour
{
    public string targetTag;
    private bool isHitting = false;
    private XRSocketTagInteractor currentSocket;
    [SerializeField] private AudioClip hammeringSound;
    private AudioSource audioSource;

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
        if (!isHitting)
        {
            if (other.CompareTag(targetTag))
            {
                Debug.Log("hammer hit detected");
                currentSocket.OnHammerHit();
                isHitting = true;
                audioSource.PlayOneShot(hammeringSound);
            }
        }

    }

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

    private void HittingCooldown()
    {
        isHitting = false;
    }
}

