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
    [SerializeField] private float videoDistanceThreshold;
    
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

        toggle = wedgeStep.GetComponentInChildren<Toggle>();
        EventManager.Instance.OnAllHoleCompleted += value =>
        {
            toggle.isOn = value;
            OnStepCompleted(wedgeStep);
        };
        
        toggle = bowlStep.GetComponentInChildren<Toggle>();
        EventManager.Instance.OnBowlCompleted += value =>
        {
            toggle.isOn = value;
            OnStepCompleted(bowlStep);
        };
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

    #region Toggle video

    public void DisplayPumpVideo()
    {
        ToggleVideo<PumpController>();
    }
    
    public void DisplayHoleVideo()
    {
        ToggleVideo<HoleController>();
    }

    public void DisplaySawVideo()
    {
        ToggleVideo<SawController>();
    }
    
    public void DisplayBowlVideo()
    {
        ToggleVideo<SocketCompletionChecker>();
    }


    #endregion

    #region Toggle Outline

    public void ShowPumpPosition()
    {
        ToggleObjectOutline<PumpController>();
    }

    public void ShowWedgesPosition()
    {
        ToggleObjectOutline<WedgeController>();
    }
    
    public void ShowHolesPosition()
    {
        ToggleObjectOutline<HoleController>();
    }

    public void ShowSawPosition()
    {
        ToggleObjectOutline<SawController>();
    }
    
    public void ShowBowlPosition()
    {
        ToggleObjectOutline<SocketCompletionChecker>();
    }

    #endregion



    private void ToggleVideo<T>() where T : MonoBehaviour
    {
        T[] objectsOfType = FindObjectsOfType<T>();

        foreach (T obj in objectsOfType)
        {
            VideoController video = obj.GetComponent<VideoController>();

            float dist = Vector3.Distance(video.gameObject.transform.position, gameObject.transform.position);

            if (Mathf.Abs(dist) <= videoDistanceThreshold)
            {
                video.ToggleVideo();
                currentTab.transform.Find("LOG").GetComponent<TMP_Text>().text = "Video is playing";
            }
            else
            {
                currentTab.transform.Find("LOG").GetComponent<TMP_Text>().text =
                    "The objective is too far to play the video";
            }

        }
    }
    
    private void ToggleObjectOutline<T>() where T : MonoBehaviour
    {
        
        T[] objectsOfType = FindObjectsOfType<T>();

        foreach (T obj in objectsOfType)
        {
            OutlineController outline = obj.GetComponent<OutlineController>();
            
            if (outline.isOn)
            {
                outline.RemoveOutline();
            }
            else
            {
                outline.ActivateOutline("Outline");
            }
        }
        

    }
    
}
