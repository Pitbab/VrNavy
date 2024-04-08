using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveParentAndCubeOnXAxisOnly : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Transform originalParent;
    private Vector3 lastPosition;
    private float initialXPosition;
    private float initialYPosition;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        originalParent = transform.parent;
        lastPosition = transform.position;
        initialXPosition = transform.position.x;
        initialYPosition = transform.position.y;

        grabInteractable.selectEntered.AddListener(Grabbed);
        grabInteractable.selectExited.AddListener(Released);
    }

    private void Grabbed(SelectEnterEventArgs arg)
    {
        lastPosition = transform.position;
    }

    private void Released(SelectExitEventArgs arg)
    {
        // Optionally, reset to initial X and Y to enforce constraints when not being interacted with
        transform.position = new Vector3(initialXPosition, initialYPosition, transform.position.z);
    }

    void Update()
    {
        if (grabInteractable.isSelected)
        {
            Vector3 currentPosition = transform.position;
            // Calculate the delta position
            Vector3 deltaPosition = currentPosition - lastPosition;
            
            // Apply the delta position only to the Z-axis of the parent socket
            if (originalParent != null)
            {
                originalParent.position += new Vector3(0, 0, deltaPosition.z);
            }
            
            // Constrain the cube's movement to the Z-axis by resetting X and Y positions
            transform.position = new Vector3(initialXPosition, initialYPosition, currentPosition.z);

            // Update the last position for the next frame
            lastPosition = transform.position;
        }
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(Grabbed);
        grabInteractable.selectExited.RemoveListener(Released);
    }
}