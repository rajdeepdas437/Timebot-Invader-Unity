using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioClip[] Music;
    private AudioSource audioSource;
    void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        
    }

    public void PlayerLevelMusic()
    {
        audioSource.clip = Music[1];
        audioSource.Play();
    }

    public void DeathMusic()
    {
        audioSource.clip = Music[0];
        audioSource.Play();
    }
}
