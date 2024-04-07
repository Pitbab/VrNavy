using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    
    [SerializeField] private List<Renderer> renderers = new List<Renderer>();
    private List<LayerMask> layers = new List<LayerMask>();
    public bool isOn;

    private void Start()
    {
        foreach (var r in renderers)
        {
            layers.Add(r.gameObject.layer);
        }
    }

    public void ActivateOutline(string layerName)
    {
        isOn = true;
        foreach (var r in renderers)
        {
            r.gameObject.layer = LayerMask.NameToLayer(layerName);
        }
    }
    
    public void RemoveOutline()
    {
        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].gameObject.layer = layers[i];
        }
        isOn = false;
    }
}
