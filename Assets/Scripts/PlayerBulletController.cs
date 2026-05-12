using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    private float BulletSpeed = 20f;
    public Rigidbody2D bulletRB;
    public GameObject bulletEffect;
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
        Instantiate(bulletEffect, this.transform.position, this.transform.rotation);       
    }

    void DestroyEffect()
    {
        Destroy(bulletEffect);
    }
}
