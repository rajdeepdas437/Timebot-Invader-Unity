using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    private float BulletSpeed = 20f;
    public Rigidbody2D bulletRB;
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bulletRB.linearVelocity = transform.right*BulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);       
    }
}
