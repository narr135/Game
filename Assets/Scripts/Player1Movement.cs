using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{

    public CharacterController1 controller;
    public Animator animator;
    public float runSpeed = 20f;
    [Header("Attack Properties")]
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private LayerMask attackMask;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    void Start()
    {

    }

    // private void OnDrawGizmos()
    // {
    //     if (_attackPoint is null){
    //         return;
    //     }

    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    // }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // OnDrawGizmos();

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        if (Input.GetButtonDown("Fire1")) {
            animator.SetBool("isAttacking", true);
        }

        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }
    }

    public void onAttacking()
    {
        animator.SetBool("isAttacking", false);
    }

    void FixedUpdate ()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
