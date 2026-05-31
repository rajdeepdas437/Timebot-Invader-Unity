using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemySwordController : MonoBehaviour
{
    public Transform player;
    private Vector3 playerDirection;
    public Rigidbody2D swordRB;
    public float swordSpeed=10f;
    private Animator swordAnim;
    private int damage=20;
    
    void Start()
    {
        player=FindAnyObjectByType<PlayerController>().transform;
        playerDirection=player.position-transform.position;
        playerDirection.Normalize();
        swordRB=GetComponent<Rigidbody2D>();
        swordAnim=GetComponent<Animator>();
    }

    void Update()
    {
        swordRB.linearVelocity=playerDirection*swordSpeed;  
    }

    public void IgnoreCollider(Collider2D enemyCollider)
    {
        Collider2D swordCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(swordCollider, enemyCollider);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        swordAnim.SetTrigger("Hit");
        swordSpeed=0;
        if(collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealthHandler>().TakeDamage(damage);
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
