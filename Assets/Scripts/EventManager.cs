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
    


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        OnHoleCompleted += CheckForAllHoleCompletion;
    }

    private void OnDestroy()
    {
        OnHoleCompleted -= CheckForAllHoleCompletion;
    }

    private void CheckForAllHoleCompletion()
    {

        HoleController[] holes = FindObjectsOfType<HoleController>();

        foreach (var hole in holes)
        {
            if (hole.GetIsPlugged() == false)
            {
                return;
            }
        }

        OnAllHoleCompleted.Invoke(true);
    }
}
