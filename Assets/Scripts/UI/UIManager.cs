using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private Animator anim;

    [SerializeField] Image weaponImage;
    [SerializeField] TextMeshProUGUI weaponName;

    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    [SerializeField] GameObject deathScreen;
    private Animator deathScreenAnim;

    [SerializeField] TextMeshProUGUI coinText;

    [SerializeField] GameObject pauseMenu;

    void Awake()
    {
        instance = this;
        anim=GetComponent<Animator>();
        coinText.text="0"; 
        StartCoroutine(UpdatePlayerUI());  
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.instance.PauseResumeGame();
        }
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
    public void TurnOnDeathScreen()
    {
        deathScreen.SetActive(true);
        deathScreenAnim=transform.Find("Death Screen").GetComponent<Animator>();
        StartCoroutine(StartBloodTrailAnim());
    }

    IEnumerator StartBloodTrailAnim()
    {
        yield return new WaitForSeconds(0.35f);
        deathScreenAnim.SetBool("isDead", true);
    }

    public void UpdateCoinUI(int newCoins)
    {
        coinText.text = newCoins.ToString();
    } 

    public void RetryButton()
    {
        LevelManager.instance.Retry();
    }

    public void MainMenuButton()
    {
        LevelManager.instance.MainMenu();
    }

    public void PauseMenu(bool onOff)
    {
        pauseMenu.SetActive(onOff);
    }

    public IEnumerator UpdatePlayerUI()
    {
        yield return new WaitForSeconds(0.1f);

        PlayerHealthHandler playerHealth = null;

        while(playerHealth == null)
        {
            playerHealth = FindAnyObjectByType<PlayerHealthHandler>();
            yield return null;
        }

        healthSlider.maxValue = playerHealth.GetMaxHealth();
        healthSlider.value = playerHealth.GetCurrentHealth();
        healthText.text = playerHealth.GetCurrentHealth() + "/" + playerHealth.GetMaxHealth();
        coinText.text = GameManager.instance.GetCurrentCoins().ToString();

        PlayerController.instance.RefreshWeaponUI();
    }

}
