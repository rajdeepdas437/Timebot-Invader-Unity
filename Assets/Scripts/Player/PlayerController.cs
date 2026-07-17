using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] int MovementSpeed=10;
    private Vector2 moveInput;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform WeaponPivot;
    [SerializeField] Camera mainCamera;
    [SerializeField] Animator playerAnim;
    
    [SerializeField] float currentSpeed;
    private bool canDash, isDashing;
    [SerializeField] float dashSpeed=20f ,dashDuration=0.3f, dashCooldown=1f;

    [SerializeField] List<WeaponSystem> availableWeapons = new List<WeaponSystem>();
    private int currentGun;

    [SerializeField] PlayerHealthHandler playerHealthHandler;

    
    void Start()
    {
        instance = this;
        rb=GetComponent<Rigidbody2D>();
        mainCamera=Camera.main;
        WeaponPivot=transform.Find("WeaponPivotPoint");
        playerAnim = GetComponent<Animator>();
        currentSpeed=MovementSpeed;
        canDash=true;
        
        for(int i=0; i<availableWeapons.Count; i++)
        {
            if(availableWeapons[i].gameObject.activeInHierarchy)
            {
                currentGun=i;
            }
        }
        
        SwitchWeaponUI(availableWeapons[currentGun]);
    }

    
    void Update()
    {
        if(LevelManager.instance.IsGamePaused())
            return;
        

        PlayerMovement();
        WeaponAim();
        PlayerAnimation();
        PlayerDash();
        SwitchGuns();

    }

    public void SwitchGuns()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(availableWeapons.Count > 0)
            {
                currentGun++;

                if(currentGun>availableWeapons.Count-1)
                {
                    currentGun=0;
                }

                foreach(WeaponSystem Weapon in availableWeapons)
                {
                    Weapon.gameObject.SetActive(false);
                    if(Weapon == availableWeapons[currentGun])
                    {
                        Weapon.gameObject.SetActive(true);
                        SwitchWeaponUI(Weapon);
                    }
                }
            }
            else
            {
                Debug.Log("No Guns available");
            }
        }
        
    }

    private static void SwitchWeaponUI(WeaponSystem Weapon)
    {
        UIManager.instance.WeaponUI(Weapon.gameObject.GetComponent<WeaponSystem>().GetGunImage(), Weapon.gameObject.GetComponent<WeaponSystem>().GetGunName());
    }

    private void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            canDash = false;
            StartCoroutine(Dash());
            StartCoroutine(DashCooldown());
            StartCoroutine(playerHealthHandler.Invincibility());    
        }
    }

    private void PlayerAnimation()
    {
        if (moveInput != Vector2.zero)
        {
            playerAnim.SetBool("isWalking", true);
        }
        else playerAnim.SetBool("isWalking", false);
    }

    private void WeaponAim()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = mainCamera.WorldToScreenPoint(transform.localPosition);
        float mouseAngle = Mathf.Atan2((mousePos.y - playerPos.y), (mousePos.x - playerPos.x)) * Mathf.Rad2Deg;
        WeaponPivot.rotation = Quaternion.Euler(0, 0, mouseAngle);
        if (mousePos.x < playerPos.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            WeaponPivot.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            WeaponPivot.localScale = Vector3.one;
        }
    }

    private void PlayerMovement()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        rb.linearVelocity = moveInput * currentSpeed;
    }

    IEnumerator Dash()
    {
        isDashing=true;
        currentSpeed=dashSpeed;
        playerAnim.SetTrigger("Dash");
        yield return new WaitForSeconds(dashDuration);
        currentSpeed=MovementSpeed;
        isDashing=false;
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    public List<WeaponSystem> GetAvailableGuns()
    {
        return availableWeapons;
    }

    public Transform GetWeaponArm()
    {
        return WeaponPivot;
    }

    public void AddGuns(WeaponSystem newWeapon)
    {
        availableWeapons.Add(newWeapon);
        currentGun=availableWeapons.Count-1;

        foreach(WeaponSystem Weapon in availableWeapons)
        {
            Weapon.gameObject.SetActive(false);
            if(Weapon == availableWeapons[currentGun])
            {
                Weapon.gameObject.SetActive(true);
                SwitchWeaponUI(Weapon);
            }
        }
    }
}
