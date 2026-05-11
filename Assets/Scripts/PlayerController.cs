using System;
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
    
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        mainCamera=Camera.main;
        WeaponPivot=transform.Find("WeaponPivotPoint");
        playerAnim = GetComponent<Animator>();
    }

    
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        rb.linearVelocity=moveInput*MovementSpeed;

        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = mainCamera.WorldToScreenPoint(transform.localPosition);
        float mouseAngle = Mathf.Atan2((mousePos.y-playerPos.y),(mousePos.x-playerPos.x))*Mathf.Rad2Deg;
        WeaponPivot.rotation = Quaternion.Euler(0,0,mouseAngle);
        if(mousePos.x<playerPos.x)
        {
            transform.localScale = new Vector3(-1f,1f,1f);
            WeaponPivot.localScale = new Vector3(-1f,-1f,1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            WeaponPivot.localScale = Vector3.one;
        }

        if(moveInput!=Vector2.zero)
        {
            playerAnim.SetBool("isWalking", true);
        }
        else playerAnim.SetBool("isWalking", false);
    }
}
