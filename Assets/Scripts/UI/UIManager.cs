using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private Animator anim;

    void Start()
    {
        instance = this;
        anim=GetComponent<Animator>();
    }

    public void InitiateFadeAnim()
    {
        anim.SetTrigger("Fade");
    }
}
