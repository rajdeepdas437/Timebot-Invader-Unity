using System.Collections;
using System.Data.Common;
using System.Runtime.CompilerServices;
using TreeEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float enemySpeed = 3f;
    public float enemyRange=3f;
    public float chaseRange=4f;
    private Rigidbody2D enemyRB;
    public Transform player;
    private Vector3 playerDirection;
    private bool isChasing;
    public Animator SkeleAnim;
    public int enemyHealth=100;
    public GameObject bloodSplatter;

    public GameObject enemyProjectile;
    private bool meleeAttack;
    private bool canShoot;
    public float throwCooldown=1f;
    private Collider2D skeleCollider;
    private Transform firePoint;
    public float shootingRange=6f;
    

    void Start()
    {
        player= FindAnyObjectByType<PlayerController>().transform;
        enemyRB=GetComponent<Rigidbody2D>();
        isChasing=false;
        SkeleAnim = GetComponentInChildren<Animator>();
        skeleCollider=GetComponent<Collider2D>();
        canShoot=true;
        meleeAttack=false;
        firePoint=transform.Find("firePoint");
    }

    void Update()
    {
        EnemyMovement();
        EnemyAnimation();
        EnemyShooting();

    }

    private void EnemyShooting()
    {
        if (!meleeAttack && canShoot && Vector3.Distance(player.position, transform.position) < shootingRange && player!=null)
        {
            SkeleAnim.SetTrigger("CanThrow");
            canShoot = false;
            StartCoroutine(ShootEnemyProjectile());
        }
    }

    IEnumerator ShootEnemyProjectile()
    {
        Instantiate(enemyProjectile, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(throwCooldown);
        enemyProjectile.GetComponent<EnemySwordController>().IgnoreCollider(skeleCollider);   
        canShoot=true;
    }

    private void EnemyAnimation()
    {
        if (isChasing)
        {
            SkeleAnim.SetBool("isWalking", true);
        }
        else
        {
            SkeleAnim.SetBool("isWalking", false);
        }
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    private void EnemyMovement()
    {
        if (Vector3.Distance(player.position, transform.position) < enemyRange)
        {
            isChasing = true;
            playerDirection = player.position - transform.position;
        }
        else if (Vector3.Distance(player.position, transform.position) < chaseRange && isChasing)
        {
            playerDirection = player.position - transform.position;
        }
        else
        {
            playerDirection = Vector3.zero;
            isChasing = false;
        }
        playerDirection.Normalize();
        enemyRB.linearVelocity = playerDirection * enemySpeed;
    }

    

    public void DamageEnemy(int damage)
    {
        enemyHealth -= damage;
        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(bloodSplatter, transform.position, transform.rotation);
        }
    }

}
