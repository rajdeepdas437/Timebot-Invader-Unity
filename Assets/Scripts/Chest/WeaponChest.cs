using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponChest : MonoBehaviour
{
    [SerializeField] WeaponPickup[] potentialWeapons;
    private SpriteRenderer chestSR;
    [SerializeField] Sprite openChestSprite;
    [SerializeField] TextMeshProUGUI openKeyText;
    [SerializeField] Transform spawnPoint;
    private bool opened=false;
    private bool canOpen;
    [SerializeField] int ChestSFX;
    void Start()
    {
        chestSR=GetComponent<SpriteRenderer>();
        openKeyText.gameObject.SetActive(false);
        canOpen=false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canOpen && !opened)
        {
            AudioManager.instance.PlaySFX(ChestSFX);
            chestSR.sprite=openChestSprite;
            int i = Random.Range(0, potentialWeapons.Count());
            Instantiate(potentialWeapons[i], spawnPoint);
            opened=true;
            openKeyText.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!opened)
        {
            openKeyText.gameObject.SetActive(true);  
        }
        canOpen=true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        openKeyText.gameObject.SetActive(false);
        canOpen=false;
    }
}
