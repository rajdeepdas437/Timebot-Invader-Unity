using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] float timeToLoad=2f;

    

    void Start()
    {
        instance=this;
    }

    public IEnumerator LoadingNextLevel(string nextLevel)
    {
        Time.timeScale=0.25f;
        UIManager.instance.InitiateFadeAnim();
        yield return new WaitForSecondsRealtime(timeToLoad);
        SceneManager.LoadScene(nextLevel);
        Time.timeScale=1f;
    }

    
}
