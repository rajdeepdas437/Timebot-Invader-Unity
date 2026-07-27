using UnityEngine;

public class BossBullets : MonoBehaviour
{
    [SerializeField] float speed;
    private Vector3 bulletDirection;
    [SerializeField] int bulletDamage;
    void Start()
    {
        bulletDirection = transform.right;
    }

    
    void Update()
    {
        transform.position += bulletDirection * speed * Time.deltaTime;

        // if(!FindAnyObjectByType<BossController>().gameObject.activeInHierarchy)
        // {
        //     Destroy(gameObject);
        // }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealthHandler>().TakeDamage(bulletDamage);
        }

        Destroy(gameObject);
    }
}
