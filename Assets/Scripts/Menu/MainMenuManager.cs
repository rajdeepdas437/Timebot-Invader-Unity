using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game exited");
    }

    public void StartGame()
    {
        int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneToLoad);
    }
}
