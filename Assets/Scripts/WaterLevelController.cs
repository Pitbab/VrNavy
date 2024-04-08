using System;
using UnityEngine;
using UnityEngine.Serialization;

public class WaterLevelController : MonoBehaviour
{

    public float defaultRiseSpeed = 1f; // Default speed at which the water rises
    public float HolePlugSpeed = 1f;
    public float PumpSpeed = 1f;
    public float BowlSpeed = 1f;
    public float maxHeight = 10f; // Maximum height the water can rise to
    private float _currentRiseSpeed; // Current water level rise speed
    public SocketCompletionChecker completionChecker; // Reference to the SocketCompletionChecker
    public PumpController pumpController;
    public HoleController holeController;
    public HoleController holeController1;
    private bool startSimulation = false;
    private bool _wasCompleted = false; // Store the previous completion state
    
    private void Start()
    {
        _currentRiseSpeed = defaultRiseSpeed;
        //completionChecker.OnCompletionStateChanged += OnCompletionStateChanged; // Subscribe to the event
        //pumpController.OnPumpActiveStateChanged += OnPumpActiveStateChanged; // Subscribe to the event
        //holeController.OnHolePluggedStateChanged += OnHolePluggedStateChanged; // Subscribe to the event
        //holeController1.OnHolePluggedStateChanged += OnHolePluggedStateChanged;
        EventManager.Instance.OnBowlCompleted += OnCompletionStateChanged;
        EventManager.Instance.OnHoleCompleted += OnHolePluggedStateChanged;
        EventManager.Instance.OnPumpCompleted += OnPumpActiveStateChanged;

    }
    
    private void OnCompletionStateChanged(bool isCompleted)
    {
        if (isCompleted)
        {
            // If the completion condition is met, decrease water level speed
            _currentRiseSpeed -= BowlSpeed; // Example: decrease speed by half
        }
        else
        {
            // If the completion condition is not met, revert to default speed
            _currentRiseSpeed += BowlSpeed;
        }
    }
    
    // Handler for PumpActiveStateChanged event
    private void OnPumpActiveStateChanged(bool isActive)
    {
        if (isActive)
        {
            // If the pump is active, decrease water level speed
            _currentRiseSpeed -= PumpSpeed;
        }
        else
        {
            // If the pump is inactive, revert to default speed
            _currentRiseSpeed += PumpSpeed;
        }
    }
    
    // Handler for OnHolePluggedStateChanged event
    private void OnHolePluggedStateChanged()
    {
        _currentRiseSpeed -= HolePlugSpeed;
    }

    public void PressButton(){
        startSimulation = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (startSimulation)
        {
            // Update water level position using current rise speed
            transform.position += Vector3.up * (_currentRiseSpeed * Time.deltaTime);
            if (transform.position.y >= maxHeight)
            {
                EventManager.Instance.SimulationLost();
                startSimulation = false;
            }
            
        }
        
    }

    private void OnDisable()
    {
        EventManager.Instance.OnBowlCompleted -= OnCompletionStateChanged;
        EventManager.Instance.OnHoleCompleted -= OnHolePluggedStateChanged;
        EventManager.Instance.OnPumpCompleted -= OnPumpActiveStateChanged;
    }
}
