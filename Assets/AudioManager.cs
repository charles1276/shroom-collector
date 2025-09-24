using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------ Audio Source --------")]
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource MusicSource;

    [Header("------ Audio Clip --------")]
    public AudioClip damage;
    public AudioClip dash;
    public AudioClip jump;
    public AudioClip mush;
    public AudioClip background;

    private void Start()
    {
        MusicSource.clip = background;
        MusicSource.Play();
    }


    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
