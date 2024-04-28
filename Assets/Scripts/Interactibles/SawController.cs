using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{
    
    private AudioSource buttonAudioSource;
    [SerializeField] private GameObject saw;
    private Slicer slicer;
    private Animation animator;
    //private string sliceAnimHash = "SlicerAnim";


    private void Start()
    {
        buttonAudioSource = GetComponentInChildren<AudioSource>();
        animator = saw.GetComponent<Animation>();
        slicer = saw.GetComponentInChildren<Slicer>();
        slicer.enabled = false;
    }

    public void PressButton()
    {
        buttonAudioSource.Play();
        slicer.enabled = true;
        animator.Play("SawAnim");
    }
}
