using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{

    public CharacterController2 controller;
    public Animator animator;
    public float runSpeed = 20f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    void Start()
    {
        
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal2") * runSpeed;

        animator.SetFloat("Speed2", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump2")){
            jump = true;
        }

        if (Input.GetButtonDown("Crouch2")){
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch2")){
            crouch = false;
        }
    }

    void FixedUpdate () 
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
