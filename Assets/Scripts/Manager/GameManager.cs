using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int currentCoins;
    public static GameManager instance;
    
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    
    void Update()
    {
        
    }

    public void GetCoins(int coins)
    {
        currentCoins += coins;
        UIManager.instance.UpdateCoinUI(currentCoins);
    }

    public void SpendCoins(int coins)
    {
        currentCoins -= coins;
        if(currentCoins < 0)
        {
            currentCoins = 0;
        }
        UIManager.instance.UpdateCoinUI(currentCoins);
    }

    public int GetCurrentCoins()
    {
        return currentCoins;
    }
}
