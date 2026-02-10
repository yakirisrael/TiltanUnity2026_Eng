using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField]
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            //first instance
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    //public void PlaySound(AudioClip clip)

    // Update is called once per frame
    public void PlayMusic(AudioClip clip, float volume = 1)
    {
        if (audioSource == null || clip == null) return;

        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();

    }
    
    public void PlaySFX(AudioClip clip, float volume = 1)
    {
        if (audioSource == null || clip == null) return;

        audioSource.volume = volume;
        audioSource.loop = false;
        audioSource.PlayOneShot(clip);
    }
}
