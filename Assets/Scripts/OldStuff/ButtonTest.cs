using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject position;
    
    public void Test()
    {
        Instantiate(prefab, position.transform);
    }
}
