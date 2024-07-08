using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource backgroundMusic;

    void Awake()
    {
        // Singleton tasar�m deseni
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Sahne ge�i�lerinde yok etme
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleMusic()
    {
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause();
        }
        else
        {
            backgroundMusic.Play();
        }
    }

    public void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;
    }
}