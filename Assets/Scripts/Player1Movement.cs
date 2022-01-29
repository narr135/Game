using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{

    public CharacterController2D controllerOne;
    public float runSpeed = 20f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    void Start()
    {
        
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump")){
            jump = true;
        }

        if (Input.GetButtonDown("Crouch")){
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch")){
            crouch = false;
        }
    }

    void FixedUpdate () 
    {
        controllerOne.Move(horizontalMove * Time.fixedDeltaTime, false, false);
        jump = false;
    }
}
