using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth=100;
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth-=damage;
        if(currentHealth<=0)
        {
            gameObject.SetActive(false);
        }
    }
}
