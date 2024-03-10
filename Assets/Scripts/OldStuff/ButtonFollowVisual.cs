using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonFollowVisual : MonoBehaviour
{
    private XRBaseInteractable interactible;
    private bool isFollowing = false;
    public Vector3 localAxis;
    private Vector3 initalLocalPos;
    public float resetSpeed = 5f;
    private bool freeze = false;
    public float followAngleThreshold;

    private Vector3 offset;
    private Transform pokeAttachTransform;
    public Transform visualTarget;
    void Start()
    {
        initalLocalPos = visualTarget.localPosition;
        interactible = GetComponent<XRBaseInteractable>();
        interactible.hoverEntered.AddListener(Follow);
        interactible.hoverExited.AddListener(ResetButton);
        interactible.selectEntered.AddListener(Freeze);
    }

    //let the button follow the poke point
    private void Follow(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;
            isFollowing = true;

            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;

            // only follow if the threshold is smaller -> to make it impossible to move the button from under
            float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));

            if (pokeAngle > followAngleThreshold)
            {
                isFollowing = false;
                freeze = true;
            }
        }
    }

    //freeze the button when pushed down completely
    private void Freeze(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            freeze = true;
        }
    }
    
    //reset the button state
    private void ResetButton(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            freeze = false;
            isFollowing = false;
        }
    }

    //update button position
    private void Update()
    {
        if(freeze) return;
        
        if (isFollowing)
        {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);
            
            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initalLocalPos, Time.deltaTime * resetSpeed);
        }
    }
}
