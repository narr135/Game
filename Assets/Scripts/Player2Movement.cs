using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : Player, IDamageble
{

    public CharacterController2 controller;
    public Animator animator;
    public float runSpeed = 20f;
    [Header("Player Properties")]
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private LayerMask _attackMask;
    public int Health {get; set;}
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    void Start()
    {

    }

    // private void OnDrawGizmos()
    // {
    //     if (_attackPoint is null) {
    //         return;
    //     }

    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    // }

    public void Damage()
    {
        Health -= 90;
        animator.SetInteger("Health2", Health);
        animator.SetBool("isAttacked2", true);
    }

    private void Attack2() 
    {
        Collider2D[] objs = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _attackMask);

        foreach (var obj in objs)
        {
            if (obj.TryGetComponent(out IDamageble hit))
            {
                hit.Damage();
                animator.SetBool("isAttacked2", false);
            }
        }
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal2") * runSpeed;

        animator.SetFloat("Speed2", Mathf.Abs(horizontalMove));


        if (Input.GetButtonDown("Jump2")) {
            jump = true;
        }

        if (Input.GetButtonDown("Fire2")) {
            animator.SetBool("isAttacking2", true);
        }

        if (Input.GetButtonDown("Crouch2")) {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch2")) {
            crouch = false;
        }
    }

    public void onAttacking2()
    {
        animator.SetBool("isAttacking2", false);
    }

    void FixedUpdate ()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
