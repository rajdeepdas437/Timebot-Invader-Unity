using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] Doors;
    private bool closeDoorOnPlayerEnter=true, openDoorsWhenEnemiesDie=true;
    [SerializeField] List<Collider2D> enemies = new List<Collider2D>();
    private Collider2D roomCollider;
    private ContactFilter2D contactFilter2D;
    
    void Start()
    {
        roomCollider=GetComponent<Collider2D>();
        contactFilter2D.SetLayerMask(LayerMask.GetMask("Enemy"));
        roomCollider.Overlap(contactFilter2D, enemies);
        AudioManager.instance.PlayerLevelMusic();
    }

    void Update()
    {
        for(int i=enemies.Count-1; i>-1; i--)
        {
            if(enemies[i]==null)
            {
                enemies.RemoveAt(i);
            }
        }
        if(enemies.Count==0)
        {
            foreach(GameObject door in Doors)
            {
                door.SetActive(false);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(closeDoorOnPlayerEnter)
            {
                foreach(GameObject door in Doors)
                {
                    door.SetActive(true);
                }
            }
        }
    }
}
