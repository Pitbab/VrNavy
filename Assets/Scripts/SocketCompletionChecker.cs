using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using System.Collections.Generic;

public class SocketCompletionChecker : MonoBehaviour
{
    public XRSocketInteractor socket1;
    public XRSocketInteractor socket2;
    public XRSocketInteractor socket3;
    private bool isCompleted = false;

    // Event to notify completion state changes
    public event Action<bool> OnCompletionStateChanged;
    [SerializeField] private List<ParticleSystem> particlesList = new List<ParticleSystem>();
    [SerializeField] private AudioSource audioSource;
    
    private void Start()
    {
        // Subscribe to events for XR Socket Interactors
        socket1.selectEntered.AddListener(OnSelectEntered);
        socket1.selectExited.AddListener(OnSelectExited);
        socket2.selectEntered.AddListener(OnSelectEntered);
        socket2.selectExited.AddListener(OnSelectExited);
        socket3.selectEntered.AddListener(OnSelectEntered);
        socket3.selectExited.AddListener(OnSelectExited);
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
        if (socket1.interactablesSelected.Count > 0 && socket2.interactablesSelected.Count > 0 && socket3.interactablesSelected.Count > 0)
        {
            isCompleted = true;
            EventManager.Instance.OnBowlCompleted?.Invoke(isCompleted);
            EventManager.Instance.OnAllCompleted?.Invoke();
            foreach (var p in particlesList)
            {
                p.Stop(true);
            }

            if (audioSource != null)
            {
                audioSource.Stop();
            }

        }
        else
        {
            isCompleted = false;
        }
    }
    
    private void SetCompletionState(bool newState)
    {
        if (newState != isCompleted)
        {
            isCompleted = newState;
            // Notify subscribers about completion state change
            OnCompletionStateChanged?.Invoke(isCompleted);
            EventManager.Instance.OnAllCompleted?.Invoke();
        }
    }

    public bool IsCompleted => isCompleted;
}