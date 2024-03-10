using UnityEngine;
using UnityEngine.Serialization;

public class WaterLevelController : MonoBehaviour
{

    public float defaultRiseSpeed = 1f; // Default speed at which the water rises
    public float maxHeight = 10f; // Maximum height the water can rise to
    private float _currentRiseSpeed; // Current water level rise speed
    public SocketCompletionChecker completionChecker; // Reference to the SocketCompletionChecker
    public PumpController pumpController;
    private bool _wasCompleted = false; // Store the previous completion state
    
    private void Start()
    {
        _currentRiseSpeed = defaultRiseSpeed;
        completionChecker.OnCompletionStateChanged += OnCompletionStateChanged; // Subscribe to the event
        pumpController.OnPumpActiveStateChanged += OnPumpActiveStateChanged; // Subscribe to the event

    }
    
    private void OnCompletionStateChanged(bool isCompleted)
    {
        if (isCompleted)
        {
            // If the completion condition is met, decrease water level speed
            _currentRiseSpeed -= 0.04f; // Example: decrease speed by half
        }
        else
        {
            // If the completion condition is not met, revert to default speed
            _currentRiseSpeed += 0.04f;
        }
    }
    
    // Handler for PumpActiveStateChanged event
    private void OnPumpActiveStateChanged(bool isActive)
    {
        if (isActive)
        {
            // If the pump is active, decrease water level speed
            _currentRiseSpeed -= 0.04f;
        }
        else
        {
            // If the pump is inactive, revert to default speed
            _currentRiseSpeed += 0.04f;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // Update water level position using current rise speed
        transform.position += Vector3.up * _currentRiseSpeed * Time.deltaTime;

        if (_wasCompleted != completionChecker.IsCompleted) // Check if completion state changed
        {
            OnCompletionStateChanged(completionChecker.IsCompleted); // Call the handler
            _wasCompleted = completionChecker.IsCompleted; // Update the stored state
        }
    }
}
