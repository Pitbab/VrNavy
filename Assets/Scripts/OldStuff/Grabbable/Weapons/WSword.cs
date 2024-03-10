using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSword : GrabbableItem
{
    [SerializeField] private Collider handle;
    
    public override void OnGrab()
    {
        handle.isTrigger = true;
    }

    public override void OnRelease()
    {
        handle.isTrigger = false;
    }
}
