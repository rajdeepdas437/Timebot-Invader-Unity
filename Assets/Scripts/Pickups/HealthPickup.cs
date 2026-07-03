using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healAmount = 10;
    private bool pickedUp=false;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !pickedUp && GetComponent<PickupDelayer>().canPickup)
        {
            pickedUp=true;   //ensures that picks up only once (fixes 2 collider issue)
            collision.GetComponent<PlayerHealthHandler>().Heal(healAmount);
            gameObject.SetActive(false);
        }
    }
}
