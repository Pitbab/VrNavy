using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WepaonSlicer : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision detected");
        SliceTesting s = collision.gameObject.GetComponent<SliceTesting>();
        if (s != null)
        {
            Debug.Log("Slice able object");
            s.SlideObject(collision.gameObject);
            //s.ShatterObject(collision.contacts[0].point, collision.contacts[0].normal);
        }
    }
}
