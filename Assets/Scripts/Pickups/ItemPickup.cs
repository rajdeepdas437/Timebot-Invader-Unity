using TreeEditor;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] bool dropsItem;
    [SerializeField] GameObject[] itemsToDrop;
    [SerializeField] float itemDropChance = 0.5f;

    public  void DropItem()
    {
        if(dropsItem)
        {
            if(Random.value < itemDropChance)
            {
                int randomItemNumber = Random.Range(0, itemsToDrop.Length);

                Instantiate(itemsToDrop[randomItemNumber], transform.position, transform.rotation);
            } 
        }
           
    }
}
