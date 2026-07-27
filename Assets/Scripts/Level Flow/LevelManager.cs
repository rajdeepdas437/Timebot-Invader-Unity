using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] float timeToLoad=2f;

    public int levelToGo_1, levelToGo_2;
    public LevelExit levelExit_1, levelExit_2;

    private bool gameIsPaused;

    [SerializeField] Transform playerSpawnPoint;

    void Start()
    {
        instance=this;
        gameIsPaused=false;
        SetPlayerSpawnPosition();
    }

    public IEnumerator LoadingNextLevel(int nextLevel)
    {
        Time.timeScale=0.25f;
        UIManager.instance.InitiateFadeAnim();
        yield return new WaitForSecondsRealtime(timeToLoad);
        SceneManager.LoadScene(nextLevel);
        Time.timeScale=1f;
    }

    public void LevelPicker()
    {
        levelToGo_1 = SceneManager.GetActiveScene().buildIndex;

        while(levelToGo_1 == SceneManager.GetActiveScene().buildIndex)
        {
            int rand = Random.Range(1, SceneManager.sceneCountInBuildSettings-1);
            print($"level to go 1 = {rand}");
            levelToGo_1 = rand;
        }

        levelExit_1.PrintLevelName(levelToGo_1);

        levelToGo_2 = SceneManager.GetActiveScene().buildIndex;

        while(levelToGo_2 == SceneManager.GetActiveScene().buildIndex || levelToGo_2 == levelToGo_1)
        {
            int rand = Random.Range(1, SceneManager.sceneCountInBuildSettings-1);
            print($"level to go 2 = {rand}");
            levelToGo_2 = rand;
        }

        levelExit_2.PrintLevelName(levelToGo_2);
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

    private void SetPlayerSpawnPosition()
    {
        PlayerController.instance.transform.position = playerSpawnPoint.position;
    }



    
}
