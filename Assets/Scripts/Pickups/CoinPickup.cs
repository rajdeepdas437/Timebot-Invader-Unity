using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int coinAmount = 10;
    private bool pickedUp=false;
    [SerializeField] int coinPickupSFXNum;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !pickedUp && GetComponent<PickupDelayer>().canPickup)
        {
            pickedUp=true;   //ensures that picks up only once (fixes 2 collider issue)
            AudioManager.instance.PlaySFX(coinPickupSFXNum);
            GameManager.instance.GetCoins(coinAmount);
            gameObject.SetActive(false);
        }
    }
}
