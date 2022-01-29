using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movment : MonoBehaviour
{

    public CharacterController2D controller1;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;

    void Start()
    {
        
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump")){
            jump = true;
        }
    }

    void FixedUpdate () 
    {
        controller1.Move(horizontalMove * Time.fixedDeltaTime, false, false);
        jump = false;
        
    }
}
