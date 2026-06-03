using UnityEngine;

public class Spikes : MonoBehaviour
{
    private Collider2D player;
    private int damage=10;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            anim.SetBool("spikeOn", true);
            player=collision;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            anim.SetBool("spikeOn", false);
        }
    }

    public void DamagePlayer()
    {
        player.GetComponent<PlayerHealthHandler>().TakeDamage(damage);   
    }
}
