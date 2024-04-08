using UnityEngine;
using UnityEngine.SceneManagement; // Include the SceneManagement namespace

public class RestartScene : MonoBehaviour
{
    // Public method to restart the current scene
    public void RestartCurrentScene()
    {
        // Get the active scene's build index
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        // Load the currently active scene again using its build index
        SceneManager.LoadScene(sceneIndex);
    }
}