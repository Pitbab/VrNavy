using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the GameObject", this);
        }
    }

    // Public method to play the audio
    public void PlayTheAudio()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
