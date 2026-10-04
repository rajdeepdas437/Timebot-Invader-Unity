using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopLevelExit : MonoBehaviour
{
    private bool canPress=false;
    [SerializeField] Canvas ShopExitCanvas;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(canPress)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                canPress=false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canPress=true;
            ShopExitCanvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canPress=false;
            ShopExitCanvas.gameObject.SetActive(false);
        }
    }

}
