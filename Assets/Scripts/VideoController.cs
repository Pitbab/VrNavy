using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPLayer;
    [SerializeField] private RawImage image;
    public bool isOn = false;

    public void ToggleVideo()
    {
        if (!isOn)
        {
            image.enabled = true;
            videoPLayer.frame = 0;
            videoPLayer.Play();
            isOn = true;
        }
        else
        {
            videoPLayer.Stop();
            image.enabled = false;
            isOn = false;
        }
    }
}
