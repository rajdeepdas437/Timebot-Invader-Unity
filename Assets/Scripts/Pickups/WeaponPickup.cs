using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] WeaponSystem weapon;
    private bool pickedUp=false;
    [SerializeField] int weaponPickupSFXNum;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            for(int i=0; i<collision.GetComponent<PlayerController>().GetAvailableGuns().Count; i++)
            {
                if(collision.GetComponent<PlayerController>().GetAvailableGuns()[i].GetGunName()==weapon.GetGunName())
                    return;
                else if(!pickedUp)
                {
                    pickedUp=true;
                    AudioManager.instance.PlaySFX(weaponPickupSFXNum);
                    WeaponSystem weaponToAdd = Instantiate(weapon, collision.GetComponent<PlayerController>().GetWeaponArm());
                    collision.GetComponent<PlayerController>().AddGuns(weaponToAdd);
                    Destroy(gameObject);
                }
            }
            
        }
    }
}
