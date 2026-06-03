using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healAmount = 10;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealthHandler>().Heal(healAmount);
            gameObject.SetActive(false);
        }
    }
}
