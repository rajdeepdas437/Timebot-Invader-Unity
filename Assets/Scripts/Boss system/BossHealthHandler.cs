using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealthHandler : MonoBehaviour
{
    [SerializeField] int bossMaxHealth = 500;
    private int bossCurrentHealth;
    private Animator bossAnim;
    private bool isInvincible=false;
    
    void Start()
    {
        bossCurrentHealth=bossMaxHealth;
        bossAnim = GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }

    public void DamageBoss(int damage)
    {
        if(!isInvincible)
        {
            bossCurrentHealth -= damage;
        }
        
        if(bossCurrentHealth <= bossMaxHealth/2)
        {
            bossAnim.SetTrigger("getAngry");
        }
        if(bossCurrentHealth<=0)
        {
            bossAnim.SetTrigger("Die");
        }
    }

    public void DestroyBoss()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Destroy(gameObject);
    }

    public int GetBossMaxHealth()
    {
        return bossMaxHealth;
    }

    public int GetBossCurrentHealth()
    {
        return bossCurrentHealth;
    }

    public void BossInvincibility(bool OnOff)
    {
        isInvincible = OnOff;
    }

}
