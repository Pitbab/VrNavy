using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class PumpController : MonoBehaviour
{
    [Header("Audio")] [SerializeField] private AudioClip ActiveSound, wheelTurningSound, buttonSound;
    private AudioSource pumpAudioSource, wheelAudioSource, buttonAudioSource;

    private bool isTurning = false;
    private bool pipeOpen = false;
    private bool pumpActive = false;
    private float startingAngle = 0f;
    private XRKnob knob;

    // Define delegate and event for pump active state change
    public delegate void PumpActiveStateChanged(bool isActive);
    public event PumpActiveStateChanged OnPumpActiveStateChanged;

    private void Start()
    {
        knob = GetComponentInChildren<XRKnob>();
        knob.onValueChange.AddListener(StartTurnWheel);
        knob.selectExited.AddListener(StopGrabbingWheel);
        
        XRGripButton button = GetComponentInChildren<XRGripButton>();
        button.onPress.AddListener(PressButton);
        
        wheelAudioSource = knob.GetComponent<AudioSource>();
        wheelAudioSource.playOnAwake = false;
        wheelAudioSource.clip = wheelTurningSound;
        wheelAudioSource.loop = true;
        
        buttonAudioSource = button.GetComponent<AudioSource>();
        buttonAudioSource.playOnAwake = false;
        buttonAudioSource.clip = buttonSound;
        buttonAudioSource.loop = false;
        
        pumpAudioSource = GetComponent<AudioSource>();
        pumpAudioSource.playOnAwake = false;
        pumpAudioSource.clip = ActiveSound;
        pumpAudioSource.loop = true;
    }

    private void PressButton()
    {
        buttonAudioSource.Play();
        
        if (pipeOpen && !pumpActive)
        {
            //adding a lerp on the volume would be nicer 
            pumpAudioSource.PlayDelayed(1f);
            pumpActive = true;
        }
        else
        {
            pumpAudioSource.Stop();
            pumpActive = false;
        }

        // Notify subscribers about pump active state change
        OnPumpActiveStateChanged?.Invoke(pumpActive);
        EventManager.Instance.OnPumpCompleted?.Invoke(pumpActive);
        EventManager.Instance.OnAllCompleted?.Invoke();
    }
    
    private void StartTurnWheel(float value)
    {
        if (!pipeOpen)
        {
            // if the wheel spin value past a certain threshold we play a sound
            if (!isTurning && Mathf.Abs(value - startingAngle) > 0.03f)
            {
                wheelAudioSource.clip = wheelTurningSound;
                wheelAudioSource.loop = true;
                wheelAudioSource.Play();
                isTurning = true;
            }
            //if the wheel spin value is too low we stop the sound
            else if(isTurning && Mathf.Abs(value - startingAngle) < 0.005f)
            {
                wheelAudioSource.Stop();
                isTurning = false;
            }
            // value at the pipe is open
            if (value > 10f && !pipeOpen)
            {
                pipeOpen = true;
                knob.enabled = false;
                wheelAudioSource.Stop();
                wheelAudioSource.clip = buttonSound;
                wheelAudioSource.loop = false;
                wheelAudioSource.Play();
            }
        }
        
        //updating the spin value to compare next cycle
        if (isTurning)
        {
            startingAngle = value;
        }

    }
    
    // stop grabbing the wheel
    private void StopGrabbingWheel(SelectExitEventArgs arg)
    {
        wheelAudioSource.Stop();
        isTurning = false;

    }
}