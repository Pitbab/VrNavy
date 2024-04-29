using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTutorialController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private bool isOn =false;

    public void ToggleAudio()
    {
        if (!isOn)
        {
            audioSource.Play();
            isOn = true;
        }
        else
        {
            audioSource.Stop();
            isOn = false;
        }
    }
}
