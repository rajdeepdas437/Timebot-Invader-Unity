using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    private float BulletSpeed = 20f;
    public Rigidbody2D bulletRB;
    public GameObject bulletEffect;
    public GameObject[] damageEffects;
    private int i;
    private int damageDealt=20;
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
        if (collision.CompareTag("Enemy"))
        {
            i=Random.Range(0,4);
            Instantiate(damageEffects[i], this.transform.position, this.transform.rotation);
            collision.GetComponent<EnemyController>().DamageEnemy(damageDealt);       
        }
        else
        {
            Instantiate(bulletEffect, this.transform.position, this.transform.rotation); 
        }
    }

}
