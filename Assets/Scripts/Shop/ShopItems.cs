using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopItems : MonoBehaviour
{
   [SerializeField] Canvas shopCanvas;
   private bool playerInsideShop = false;

   enum ItemType
    {
        healthPickup,
        healthUpgrade,
        Weapon
    }

    [SerializeField] ItemType itemType;
    [SerializeField] int itemCost;

    [SerializeField] WeaponSystem[] potentialWeaponToBuy;
    [SerializeField] TextMeshProUGUI weaponPrice;
    [SerializeField] SpriteRenderer weaponSprite;
    private WeaponSystem weaponToBuy;

    private PlayerHealthHandler playerHealthHandler;

    void Start()
    {
        if(itemType == ItemType.Weapon)
        {
            int weaponNumber = Random.Range(0, potentialWeaponToBuy.Length);
            weaponToBuy = potentialWeaponToBuy[weaponNumber];
            itemCost = weaponToBuy.GetWeaponPrice();
            weaponSprite.sprite = weaponToBuy.GetWeaponShopSprite();

            weaponPrice.text = $"{weaponToBuy.GetGunName()} : {itemCost} Coins";
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerInsideShop)
        {
            if(GameManager.instance.GetCurrentCoins()>=itemCost)
            {
                GameManager.instance.SpendCoins(itemCost);

                switch(itemType)
                {
                    case ItemType.healthPickup:
                        playerHealthHandler.Heal(10);
                        AudioManager.instance.PlaySFX(4);
                        break;

                    case ItemType.healthUpgrade:
                        playerHealthHandler.IncreaseMaxHP(50);
                        AudioManager.instance.PlaySFX(4);
                        break;
                    
                    case ItemType.Weapon:
                        PlayerController player = playerHealthHandler.GetComponent<PlayerController>();
                        WeaponSystem weaponBought = Instantiate(weaponToBuy, player.GetWeaponArm());
                        player.AddGuns(weaponBought);
                        break;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            shopCanvas.gameObject.SetActive(true);
            playerInsideShop=true;
            playerHealthHandler = collision.GetComponent<PlayerHealthHandler>();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            shopCanvas.gameObject.SetActive(false);
            playerInsideShop=false;
        }
    }
}
