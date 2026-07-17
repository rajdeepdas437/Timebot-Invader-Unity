using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] float timeToLoad=2f;

    private bool gameIsPaused;

    void Start()
    {
        instance=this;
        gameIsPaused=false;
    }

    public IEnumerator LoadingNextLevel(string nextLevel)
    {
        Time.timeScale=0.25f;
        UIManager.instance.InitiateFadeAnim();
        yield return new WaitForSecondsRealtime(timeToLoad);
        SceneManager.LoadScene(nextLevel);
        Time.timeScale=1f;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public bool IsGamePaused()
    {
        return gameIsPaused;
    }

    public void PauseResumeGame()
    {
        if(!gameIsPaused)
        {
            UIManager.instance.PauseMenu(true);
            gameIsPaused=true;
            Time.timeScale=0f;
        }
        else
        {
            UIManager.instance.PauseMenu(false);
            gameIsPaused=false;
            Time.timeScale=1f;
        }
    }



    
}
