using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] Slider bossHealthSlider;
    
    void Start()
    {
        bossHealthSlider.maxValue = FindAnyObjectByType<BossHealthHandler>().GetBossMaxHealth();
    }

    
    void Update()
    {
        if(FindAnyObjectByType<BossHealthHandler>() != null)
        {
            bossHealthSlider.value = FindAnyObjectByType<BossHealthHandler>().GetBossCurrentHealth();

            if(bossHealthSlider.value <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    
}
