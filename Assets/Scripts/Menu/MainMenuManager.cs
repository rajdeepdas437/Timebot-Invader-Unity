using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] GameObject LoadingUI;
    [SerializeField] Slider LoadBar;
    
    void Awake()
    {
        // player.gameObject.GetComponent<PlayerController>().enabled=false;
        // player.gameObject.GetComponent<PlayerHealthHandler>().enabled=false;
        // player.gameObject.GetComponentInChildren<WeaponSystem>().enabled=false;
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
        Time.timeScale=1f;

        // player.gameObject.GetComponent<PlayerController>().enabled=true;
        // player.gameObject.GetComponent<PlayerHealthHandler>().enabled=true;
        // player.gameObject.GetComponentInChildren<WeaponSystem>().enabled=true;

        int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        // SceneManager.LoadScene(sceneToLoad);

        LoadingUI.SetActive(true);
        StartCoroutine(LoadAsync(sceneToLoad));

        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.GetComponent<PlayerHealthHandler>().ResetPlayerHealth();
        PlayerController.instance.ResetPlayerWeapons();
        GameManager.instance.ResetCoins();
    }

    IEnumerator LoadAsync(int sceneToLoad)
    {
        float minLoadTime = 0.5f;
        float timer = 0f;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            timer += Time.unscaledDeltaTime;

            float realProgress = Mathf.Clamp01(operation.progress/0.9f);
            float timeProgress = Mathf.Clamp01(timer/minLoadTime);
            float targetProgress = Mathf.Min(realProgress, timeProgress);

            LoadBar.value = Mathf.Lerp(LoadBar.value, targetProgress, Time.unscaledDeltaTime*8);
            if(operation.progress >= 0.9f && timer >= minLoadTime)
            {
                operation.allowSceneActivation=true;
            }

            yield return null;
        }

    }
}
