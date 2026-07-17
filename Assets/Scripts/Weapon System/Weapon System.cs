using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public float fireCooldown = 0.5f;
    private float fireCounter;

    [SerializeField] Sprite weaponImage;
    [SerializeField] string weaponName;
    [SerializeField] int SFXNum;

    [SerializeField] int weaponPrice;
    [SerializeField] Sprite weaponShopSprite;
    

    void Start()
    {
        firePoint=transform.Find("Fire Point");
    }

    void Update()
    {
        if(LevelManager.instance.IsGamePaused())
            return;
            
        if (Input.GetMouseButton(0) & fireCounter <= 0)
        {
            AudioManager.instance.PlaySFX(SFXNum);
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            fireCounter = fireCooldown;
        }
        if (fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
    }

    public Sprite GetGunImage()
    {
        return weaponImage;
    }
    public string GetGunName()
    {
        return weaponName;
    }

    public int GetWeaponPrice()
    {
        return weaponPrice;
    }

    public Sprite GetWeaponShopSprite()
    {
        return weaponShopSprite;
    }


    
}
