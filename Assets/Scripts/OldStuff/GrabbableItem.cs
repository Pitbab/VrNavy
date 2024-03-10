using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrabbableItem : MonoBehaviour
{
    public abstract void OnGrab();


    public abstract void OnRelease();

}
