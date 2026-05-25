using System.Runtime.CompilerServices;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    [SerializeField] GameObject[] brokenPieces;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerController>().IsDashing())
            {
                GetComponent<Animator>().SetTrigger("Break");
            }    
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player Bullet"))
        {
            GetComponent<Animator>().SetTrigger("Break");
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
}
