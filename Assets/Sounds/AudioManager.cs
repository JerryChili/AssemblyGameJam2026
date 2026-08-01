using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip itemPickup;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource ambienceSource;



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;

        DontDestroyOnLoad(gameObject);
    }



    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
            return;


        sfxSource.PlayOneShot(clip);
    }


    public void PlayAmbience(AudioClip clip)
    {
        if (clip == null)
            return;


        ambienceSource.clip = clip;
        ambienceSource.loop = true;
        ambienceSource.Play();
    }


    public void PlayItemPickupSound()
    {
        if (itemPickup == null)
            return;


        sfxSource.PlayOneShot(itemPickup);
    }
}