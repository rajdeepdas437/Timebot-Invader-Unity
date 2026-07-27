using UnityEngine;

public class eyeProjectile : MonoBehaviour
{
    public Transform player;
    private Vector3 playerDirection;
    public Rigidbody2D projectileRB;
    public float projectileSpeed=10f;
    private int damage=20;
    
    void Start()
    {
        player=FindAnyObjectByType<PlayerController>().transform;
        playerDirection=player.position-transform.position;
        playerDirection.Normalize();
        projectileRB=GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        projectileRB.linearVelocity=playerDirection*projectileSpeed;  
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        projectileSpeed=0;
        if(collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealthHandler>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
