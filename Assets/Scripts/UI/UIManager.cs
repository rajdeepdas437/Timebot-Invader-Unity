using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private Animator anim;

    [SerializeField] Image weaponImage;
    [SerializeField] TextMeshProUGUI weaponName;

    void Start()
    {
        instance = this;
        anim=GetComponent<Animator>();
    }

    public void InitiateFadeAnim()
    {
        anim.SetTrigger("Fade");
    }

    public void WeaponUI(Sprite gunImage, string gunName)
    {
        weaponImage.sprite = gunImage;
        weaponName.text = gunName;
    }
}
