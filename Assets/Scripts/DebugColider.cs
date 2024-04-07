using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugColider : MonoBehaviour
{
    void Start()
    {
        // Get the Collider component attached to this GameObject
        Collider collider = GetComponent<Collider>();

        // Check if the GameObject has a Collider component
        if (collider != null)
        {
            // Get the size of the collider's bounding box
            Vector3 size = collider.bounds.size;
            
            // Print the size to the Unity console
            Debug.Log("Collider Size: " + size);
        }
        else
        {
            // Print a warning if no Collider is found
            Debug.LogWarning("No Collider attached to " + gameObject.name);
        }
    }
}