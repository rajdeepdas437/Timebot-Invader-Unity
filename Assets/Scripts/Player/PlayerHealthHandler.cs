using System.Collections;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth=100;
    [SerializeField] float invincibilityDuation=2f;
    private bool isInvincible;
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] int damageSFXNum;
    void Start()
    {
        currentHealth = maxHealth;
        UIManager.instance.healthSlider.maxValue=maxHealth;
        UIManager.instance.healthSlider.value=currentHealth;
        UIManager.instance.healthText.text=currentHealth+"/"+maxHealth;
        isInvincible=false;
    }

    void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        if(!isInvincible)
        {
            AudioManager.instance.PlaySFX(damageSFXNum);
            currentHealth-=damage;
            UIManager.instance.healthSlider.value=currentHealth;
            UIManager.instance.healthText.text=currentHealth+"/"+maxHealth;
            if(currentHealth<=0)
            {
                UIManager.instance.TurnOnDeathScreen();
                AudioManager.instance.DeathMusic();
                gameObject.SetActive(false);
            }
            StartCoroutine(Invincibility());
        }
        
    }

    public IEnumerator Invincibility()
    {
        isInvincible=true;
        StartCoroutine(SpriteFlashing());
        yield return new WaitForSeconds(invincibilityDuation);
        isInvincible=false;
    }

    IEnumerator SpriteFlashing()
    {
        for(int i=0; i<9; i++)
        {
            playerSprite.color = new Color(

                playerSprite.color.r,
                playerSprite.color.g,
                playerSprite.color.b,
                0f
            );

            yield return new WaitForSeconds(0.1f);

            playerSprite.color = new Color(

                playerSprite.color.r,
                playerSprite.color.g,
                playerSprite.color.b,
                1f
            );

            yield return new WaitForSeconds(0.1f);
        }
        
    }

    public void Heal(int healAmount)
    {
        currentHealth+=healAmount;
        if(currentHealth>maxHealth)
        {
            currentHealth=maxHealth;
        }
        UIManager.instance.healthSlider.value=currentHealth;
        UIManager.instance.healthText.text=currentHealth+"/"+maxHealth;
    }

    public void IncreaseMaxHP(int maxHealthAmount)
    {
        maxHealth += maxHealthAmount;
        currentHealth = maxHealth;
        UIManager.instance.healthSlider.maxValue=maxHealth;
        UIManager.instance.healthSlider.value=currentHealth;
        UIManager.instance.healthText.text=currentHealth+"/"+maxHealth;
    }
}
