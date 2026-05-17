using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int MovementSpeed=10;
    private Vector2 moveInput;
    public Rigidbody2D rb;
    public Transform WeaponPivot;
    public Camera mainCamera;
    public Animator playerAnim;

    public GameObject bullet;
    public Transform firePoint;
    public float fireCooldown = 0.5f;
    private float fireCounter;
    
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        mainCamera=Camera.main;
        WeaponPivot=transform.Find("WeaponPivotPoint");
        playerAnim = GetComponent<Animator>();
        firePoint=transform.Find("WeaponPivotPoint/Fire Point");
        fireCounter=fireCooldown;
    }

    
    void Update()
    {
        PlayerMovement();
        WeaponAim();
        PlayerAnimation();
        PlayerShooting();
    }

    private void PlayerShooting()
    {
        if (Input.GetMouseButton(0) & fireCounter <= 0)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            fireCounter = fireCooldown;
        }
        if (fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
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
        rb.linearVelocity = moveInput * MovementSpeed;
    }
}
