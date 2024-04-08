using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSocketPosition : MonoBehaviour
{
    public GameObject parent;

    private void Update()
    {
        if (parent != null)
        {
            transform.position = parent.transform.position;
        }
    }
}
