// Dateipfad: Scripts/AudioManager.cs
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource; // F�r Hintergrundmusik
    [SerializeField] private AudioSource effectsSource; // F�r Soundeffekte

    private void Awake()
    {
        // Singleton-Logik
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Doppelte Instanz wird zerst�rt
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Verhindert, dass das Objekt bei Szenenwechseln zerst�rt wird
    }

    // Spielt Hintergrundmusik ab
    public void PlayMusic(AudioClip clip, float volume = 1.0f)
    {
        if (musicSource.clip == clip)
            return; // Verhindert das erneute Starten derselben Musik

        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Stoppt die Hintergrundmusik
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Spielt einen Soundeffekt ab
    public void PlayEffect(AudioClip clip, float volume = 1.0f)
    {
        effectsSource.PlayOneShot(clip, volume);
    }

    // Setzt die Lautst�rke f�r Musik
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    // Setzt die Lautst�rke f�r Soundeffekte
    public void SetEffectsVolume(float volume)
    {
        effectsSource.volume = volume;
    }
}
