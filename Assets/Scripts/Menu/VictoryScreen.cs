using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{

    
    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
    }
}
