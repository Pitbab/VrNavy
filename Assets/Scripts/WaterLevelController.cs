using UnityEngine;

public class WaterLevelController : MonoBehaviour
{

    public float defaultRiseSpeed = 1f; // Default speed at which the water rises
    public float maxHeight = 10f; // Maximum height the water can rise to
    private float _currentRiseSpeed; // Current water level rise speed
    public GameObject completionChecker; // Reference to the game object with XR Socket Interactors

    private void Start()
    {
        _currentRiseSpeed = defaultRiseSpeed;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (completionChecker.GetComponent<SocketCompletionChecker>().getIsCompleted() && transform.position.y < maxHeight)
        {
            // If the completion condition is met, decrease water level speed
            _currentRiseSpeed = 0.01f;
        }
        else
        {
            _currentRiseSpeed = 0.1f;
        }

        // Update water level position using current rise speed
        transform.position += Vector3.up * _currentRiseSpeed * Time.deltaTime;
    }
}
