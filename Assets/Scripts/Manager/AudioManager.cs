using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioClip[] Music;
    [SerializeField] GameObject[] SFX;
    private AudioSource audioSource;
    private GameObject tempSFX;
    void Awake()
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

    public void PlaySFX(int SFXnum)
    {
        // AudioSource.PlayClipAtPoint(SFX[SFXnum], Camera.main.transform.position);
        tempSFX = Instantiate(SFX[SFXnum], Camera.main.transform);
        float clipDuration = tempSFX.GetComponent<AudioSource>().clip.length;
        Invoke(nameof(StopSFX), clipDuration);
    }

    void StopSFX()
    {
        Destroy(tempSFX);
    }
}
