using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    
    public Action<bool> OnPumpCompleted;
    public Action OnHoleCompleted;
    public Action<bool> OnAllHoleCompleted;
    public Action<bool> OnBowlCompleted;
    public Action OnAllCompleted;
    public int totalStepToComplete = 3;
    private int numberOfStepDone = 0;
    [SerializeField] private AudioClip CompleteSound, failedSound;
    private AudioSource audioSource;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        OnHoleCompleted += CheckForAllHoleCompletion;
        OnAllCompleted += CheckForEnd;
    }

    private void OnDestroy()
    {
        OnHoleCompleted -= CheckForAllHoleCompletion;
        OnAllCompleted -= CheckForEnd;
    }

    private void CheckForAllHoleCompletion()
    {

        HoleController[] holes = FindObjectsOfType<HoleController>();
        Debug.Log("check for all hole compeleted");

        foreach (var hole in holes)
        {
            if (hole.GetIsPlugged() == false)
            {
                Debug.Log(hole.gameObject + " " + hole.GetIsPlugged());
                return;
            }
        }

        OnAllHoleCompleted?.Invoke(true);
        OnAllCompleted?.Invoke();
    }

    private void CheckForEnd()
    {
        numberOfStepDone++;

        Debug.Log("entered completion check");
        if (numberOfStepDone >= totalStepToComplete)
        {
            audioSource.PlayOneShot(CompleteSound);
            Debug.Log("you won!");
        }
    }

    public void SimulationLost()
    {
        audioSource.PlayOneShot(failedSound);
    }
}
