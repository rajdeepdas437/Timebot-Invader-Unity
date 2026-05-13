using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float enemySpeed = 3f;
    public float enemyRange=2f;
    public float chaseRange=4f;
    private Rigidbody2D enemyRB;
    public Transform player;
    private Vector3 playerDirection;
    private bool isChasing;

    void Start()
    {
        player= FindAnyObjectByType<PlayerController>().transform;
        enemyRB=GetComponent<Rigidbody2D>();
        isChasing=false;
    }

    void Update()
    {
        if(Vector3.Distance(player.position, transform.position) < enemyRange)
        {
            isChasing=true;
            playerDirection=player.position-transform.position;
        }
        else if(Vector3.Distance(player.position, transform.position) < chaseRange && isChasing)
        {
            playerDirection=player.position-transform.position;
        }
        else
        {
            playerDirection=Vector3.zero;
            isChasing=false;
        }
        playerDirection.Normalize();
        enemyRB.linearVelocity=playerDirection*enemySpeed;
    }

    void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyRange);

        Gizmos.color=Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

}
