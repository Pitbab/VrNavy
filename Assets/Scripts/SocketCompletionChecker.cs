using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketCompletionChecker : MonoBehaviour
{
    public XRSocketInteractor socket1;
    public XRSocketInteractor socket2;
    private bool isCompleted = false;

    private void Start()
    {
        // Subscribe to events for XR Socket Interactors
        socket1.selectEntered.AddListener(OnSelectEntered);
        socket1.selectExited.AddListener(OnSelectExited);
        socket2.selectEntered.AddListener(OnSelectEntered);
        socket2.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Check completion condition when an object is attached
        CheckCompletion();
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // Check completion condition when an object is detached
        CheckCompletion();
    }

    private void CheckCompletion()
    {
        // Check if both sockets are filled
        if (socket1.interactablesSelected.Count > 0 && socket2.interactablesSelected.Count > 0)
        {
            isCompleted = true;
        }
        else
        {
            isCompleted = false;
        }
    }

    public bool getIsCompleted()
    {
        return isCompleted;
    }
}