using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClipBoardController : MonoBehaviour
{
    [SerializeField] private GameObject tutorialTab;
    [SerializeField] private GameObject pumpStep, wedgeStep, bowlStep;
    
    private GameObject currentTab;
    

    private void Start()
    {
        currentTab = tutorialTab;
        InitUI();
    }

    private void InitUI()
    {
        // link toggle to task completion
        
        Toggle toggle = pumpStep.GetComponentInChildren<Toggle>();
        EventManager.Instance.OnPumpCompleted += value =>
        {
            toggle.isOn = value;
            OnStepCompleted(pumpStep);
        };

        //still need to set up these with main level
        /*
        toggle = wedgeStep.GetComponentInChildren<Toggle>();
        //toggle.SetIsOnWithoutNotify(false);
        EventManager.Instance.OnAllHoleCompleted += toggle.onValueChanged.Invoke;
        toggle.onValueChanged.AddListener(_=>OnStepCompleted(wedgeStep));
        toggle.onValueChanged.AddListener(value =>toggle.isOn = value);
        
        /*
        toggle = bowlStep.GetComponentInChildren<Toggle>();
        //toggle.SetIsOnWithoutNotify(false);
        EventManager.Instance.OnBowlCompleted += toggle.onValueChanged.Invoke;
        toggle.onValueChanged.AddListener(_=>OnStepCompleted(bowlStep));
        toggle.onValueChanged.AddListener(value =>toggle.isOn = value);
        */

    }

    private void OnStepCompleted(GameObject step)
    {
        string currentText;
        TMP_Text text = step.GetComponentInChildren<TMP_Text>();
        currentText = text.text;
        currentText = $"<s>{currentText}</s>";
        text.text = currentText;

    }

    public void SwitchCurrentTab(GameObject newTab)
    {
        currentTab.SetActive(false);
        currentTab = newTab;
        currentTab.SetActive(true);
    }
    
}
