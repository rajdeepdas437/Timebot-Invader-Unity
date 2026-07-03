using System.Runtime.CompilerServices;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    [SerializeField] GameObject[] brokenPieces;
    [SerializeField] int breakSFXNum;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerController>().IsDashing())
            {
                PlayBreakSFX();
                GetComponent<Animator>().SetTrigger("Break");
                if(GetComponent<ItemPickup>()!=null)
                {
                    GetComponent<ItemPickup>().DropItem();
                }
            }    
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player Bullet"))
        {
            PlayBreakSFX();
            GetComponent<Animator>().SetTrigger("Break");
            if(GetComponent<ItemPickup>()!=null)
                {
                    GetComponent<ItemPickup>().DropItem();
                }
        }
    }

    void Destroy()
    {
        for(int i=0; i<brokenPieces.Length; i++)
        {
            float randomRotation = Random.Range(0f,4f);
            Instantiate(brokenPieces[i], transform.position, Quaternion.Euler(0f,0f,90f*randomRotation));
        }
        
        
        Destroy(gameObject);
    }

    public void PlayBreakSFX()
    {
        AudioManager.instance.PlaySFX(breakSFXNum);
    }
}
