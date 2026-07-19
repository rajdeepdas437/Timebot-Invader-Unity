using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Transform playerToChase;
    [SerializeField] int damageAmount = 20;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRadius=1.5f;
    [SerializeField] LayerMask whatIsPlayer;

    [SerializeField] int angryDamageAmount=40;
    [SerializeField] float angryAttackRadius=2.4f;

    [SerializeField] Transform[] shootingPoints;
    [SerializeField] Transform[] angryShootingPoints;
    [SerializeField] GameObject bossBullets;

    void Start()
    {
        playerToChase = FindAnyObjectByType<PlayerController>().transform;
        AudioManager.instance.PlayerLevelMusic();    
    }

    
    void Update()
    {
        if(playerToChase.position.x - transform.position.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f,180f,0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f,0f,0f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    public void Attack()
    {
        Collider2D playerToAttack = Physics2D.OverlapCircle(attackPoint.position, attackRadius, whatIsPlayer);

        if(playerToAttack != null)
        {
            playerToAttack.GetComponent<PlayerHealthHandler>().TakeDamage(damageAmount);
        }
    }

    public void AngryAttack()
    {
        Collider2D playerToAttack = Physics2D.OverlapCircle(attackPoint.position, angryAttackRadius, whatIsPlayer);

        if(playerToAttack != null)
        {
            playerToAttack.GetComponent<PlayerHealthHandler>().TakeDamage(angryDamageAmount);
        }
    }

    public void BossShooting()
    {
        foreach(Transform point in shootingPoints) 
        {
            Instantiate(bossBullets, point.position, point.rotation);
        }
    }

    public void BossAngryShooting()
    {
        foreach(Transform angryPoint in angryShootingPoints)
        {
            Instantiate(bossBullets, angryPoint.position, angryPoint.rotation);
        }
    }


}
