using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UnderWaterCheck : MonoBehaviour
{
    public LayerMask waterLayer;
    private int frameCounter = 0;
    [SerializeField] private GameObject postProcessVolume;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            Debug.Log("entered water");
            postProcessVolume.GetComponent<Volume>().weight = 1;
        }
    }
    
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            Debug.Log("leaving water");
            postProcessVolume.GetComponent<Volume>().weight = 0;
        }
    }
}
