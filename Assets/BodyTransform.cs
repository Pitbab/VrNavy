using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;

public class BodyTransform : MonoBehaviour
{
    private void Start()
    {
        XRBodyTransformer bodyTransformer = GetComponent<XRBodyTransformer>();

        bodyTransformer.useCharacterControllerIfExists = true;
    }
}
