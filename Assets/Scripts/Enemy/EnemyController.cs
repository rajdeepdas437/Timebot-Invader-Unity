using System.Collections;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using TreeEditor;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int enemyHealth=100;
    private Rigidbody2D enemyRB;
    [SerializeField] float enemySpeed = 3f;
    private Collider2D enemyCollider;
    private Vector3 directionToMoveIn;

    //enemies that chase player:-
    [SerializeField] bool shouldChasePlayer;
    [SerializeField] float enemyRange=3f;
    [SerializeField] float chaseRange=4f;
    public Transform player;
    private bool isChasing;
    private Animator enemyAnim;
    public GameObject bloodSplatter;

    //Attack:-
    public GameObject enemyProjectile;
    private bool meleeAttack;
    private bool canShoot;
    public float throwCooldown=1f;
    private Transform firePoint;
    public float shootingRange=6f;

    //SFX
    [SerializeField] int enemyDamageSFXNum;

    //enemies that run away :-
    [SerializeField] bool shouldRunAway;
    [SerializeField] float runawayRange;

    //enemies that wander:-
    [SerializeField] bool shouldWander;
    [SerializeField] float wanderLength, pauseLength;
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;
    
    //enemies that patroll
    [SerializeField] bool shouldPatroll;
    [SerializeField] Transform[] patrollPoints;
    private int currentPatrollPoint;

    

    void Start()
    {
        player= FindAnyObjectByType<PlayerController>().transform;
        enemyRB=GetComponent<Rigidbody2D>();
        isChasing=false;
        enemyAnim = GetComponentInChildren<Animator>();
        enemyCollider=GetComponent<Collider2D>();
        canShoot=true;
        meleeAttack=false;
        firePoint=transform.Find("firePoint");

        if(shouldWander)
        {
            pauseCounter = Random.Range(pauseLength*0.5f, pauseLength*1.75f);
        }
    }

    void Update()
    {
        EnemyMovement();
        EnemyAnimation();
        EnemyShooting();
    }

    private void EnemyShooting()
    {
        if(LevelManager.instance.IsGamePaused())
            return;
            
        float playerEnemyDistance = Vector3.Distance(player.position, transform.position);

        if (!meleeAttack && canShoot && playerEnemyDistance < shootingRange && player.gameObject.activeInHierarchy)
        {
            enemyAnim.SetTrigger("CanThrow");
            canShoot = false;
            StartCoroutine(ShootEnemyProjectile());
        }
    }

    IEnumerator ShootEnemyProjectile()
    {
        Instantiate(enemyProjectile, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(throwCooldown);   
        canShoot=true;
    }

    private void EnemyAnimation()
    {
        if(shouldChasePlayer)
        {
            // if (isChasing)
            // {
            //     enemyAnim.SetBool("isWalking", true);
            // }
            // else
            // {
            //     enemyAnim.SetBool("isWalking", false);
            // }

            if(directionToMoveIn != Vector3.zero)
            {
                enemyAnim.SetBool("isWalking", true);
            }
            else
            {
                enemyAnim.SetBool("isWalking", false);
            }
        }
        
        if(isChasing)
        {
            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }
        
    }

    private void EnemyMovement()
    {
        float playerEnemyDistance = Vector3.Distance(player.position, transform.position);

        if (playerEnemyDistance < enemyRange && shouldChasePlayer)
        {
            isChasing = true;
            directionToMoveIn = player.position - transform.position;
        }
        else if (playerEnemyDistance < chaseRange && isChasing && shouldChasePlayer)
        {
            directionToMoveIn = player.position - transform.position;
        }
        else
        {
            directionToMoveIn = Vector3.zero;
            isChasing = false;
        }

        if(shouldWander && !isChasing)
        {
            if(wanderCounter>0)
            {
                wanderCounter -= Time.deltaTime;

                directionToMoveIn = wanderDirection;

                if(wanderDirection.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else
                {
                    transform.localScale = Vector3.one;
                }

                if(wanderCounter <=0)
                {
                    pauseCounter = Random.Range(pauseLength*0.5f, pauseLength*1.75f);
                }
            }

            if(pauseCounter > 0)
            {
                pauseCounter -= Time.deltaTime;

                if(pauseCounter <= 0)
                {
                    wanderDirection = new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), 0f);
                    wanderCounter = Random.Range(wanderLength*0.5f, wanderLength*1.25f);
                }
            }
            Debug.Log(directionToMoveIn);
        }

        if(shouldRunAway && playerEnemyDistance < runawayRange)
        {
            directionToMoveIn = transform.position - player.position;
        }

        if(shouldPatroll && !isChasing)
        {
            directionToMoveIn = patrollPoints[currentPatrollPoint].position - transform.position;

            float distanceBetweenEnemyAndPatrollPoint = (patrollPoints[currentPatrollPoint].position - transform.position).magnitude;
            if(distanceBetweenEnemyAndPatrollPoint < 0.1f)
            {
                currentPatrollPoint++;

                if(currentPatrollPoint >= patrollPoints.Length)
                    currentPatrollPoint=0;
            }

            if(directionToMoveIn.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }
        directionToMoveIn.Normalize();
        enemyRB.linearVelocity = directionToMoveIn * enemySpeed;
    }

    

    public void DamageEnemy(int damage)
    {
        enemyHealth -= damage;
        AudioManager.instance.PlaySFX(enemyDamageSFXNum);
        if(enemyHealth <= 0)
        {
            if(shouldRunAway)
            {
                enemyAnim.SetTrigger("Death");
                StartCoroutine(DestroyEnemy());
            }

            if(shouldChasePlayer)
            {
                Destroy(gameObject);
                Instantiate(bloodSplatter, transform.position, transform.rotation);
            }

            if(GetComponent<ItemPickup>()!=null)
                {
                    GetComponent<ItemPickup>().DropItem();
                }
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(shouldRunAway)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position ,runawayRange);
        }

        if(shouldChasePlayer)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, shootingRange);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseRange);

            Gizmos.color = Color.orange;
            Gizmos.DrawWireSphere(transform.position, enemyRange);
        }       
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
