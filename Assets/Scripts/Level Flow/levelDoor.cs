using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class levelDoor : MonoBehaviour
{
    [SerializeField] Canvas doorCanvas;
    [SerializeField] TextMeshProUGUI levelText;
    private bool canOpenDoor=false;
    [SerializeField] WaveSpawnManager waveScript;
    private bool enemiesDefeated;

    void Start()
    {
        doorCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        enemiesDefeated = waveScript.IsWaveCompleted();

        if(canOpenDoor && enemiesDefeated)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && enemiesDefeated)
        {
            doorCanvas.gameObject.SetActive(true);
            canOpenDoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && enemiesDefeated)
        {
            doorCanvas.gameObject.SetActive(false);
            canOpenDoor = false;
        }
    }

    public void SetDoorLevelName(string lvlName)
    {
        levelText.text = lvlName;
    }
}
