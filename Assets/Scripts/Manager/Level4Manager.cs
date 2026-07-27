using System.Collections.Generic;
using UnityEngine;

public class Level4Manager : MonoBehaviour
{
    [SerializeField] List<EnemyController> enemies = new List<EnemyController>();
    [SerializeField] WeaponChest treasureChest;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            foreach (EnemyController enemy in enemies)
            {
                enemy.gameObject.SetActive(true);
                treasureChest.gameObject.SetActive(false);
            }
            
        }
    }

    void Update()
    {

        for(int i=0; i<enemies.Count; i++)
        {
            if(enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }

        if(enemies.Count == 0)
        {
            treasureChest.gameObject.SetActive(true);
        }

        
    }

    
}
