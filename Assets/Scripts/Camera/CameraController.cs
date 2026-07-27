using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerController playerToLookAt;
    CinemachineCamera cineCamera;
    void Start()
    {
        playerToLookAt = FindAnyObjectByType<PlayerController>();
        cineCamera = GetComponent<CinemachineCamera>();

        cineCamera.Follow = playerToLookAt.transform;
    }

    
    void Update()
    {
        while(playerToLookAt == null)
        {
            playerToLookAt = FindAnyObjectByType<PlayerController>();
            cineCamera.Follow = playerToLookAt.transform;
        }
    }
}
