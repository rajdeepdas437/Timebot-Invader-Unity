using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healAmount = 10;
    private bool pickedUp=false;
    [SerializeField] int healthPickupSFXNum;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !pickedUp && GetComponent<PickupDelayer>().canPickup)
        {
            pickedUp=true;   //ensures that picks up only once (fixes 2 collider issue)
            AudioManager.instance.PlaySFX(healthPickupSFXNum);
            collision.GetComponent<PlayerHealthHandler>().Heal(healAmount);
            gameObject.SetActive(false);
        }
    }
}
