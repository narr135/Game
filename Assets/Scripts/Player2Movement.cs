using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Movement : MonoBehaviour
{

    public CharacterController2 controller;
    public Animator animator;
    public Animator enemyAnimator;
    [Header("Player Properties")]
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private float _attackRange = 0.5f;
    [SerializeField]
    private LayerMask _attackMask;
    [SerializeField]
    private float runSpeed = 20f;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    public static  bool isDead2 = false;
    public static float health2;

    void Start()
    {
        health2 = hp2.mHealth;
    }

    // private void OnDrawGizmos()
    // {
    //     if (_attackPoint is null) {
    //         return;
    //     }

    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    // }

    public void Damaged2()
    {
        Player1Movement.health -= 30f;
        enemyAnimator.SetTrigger("isAttacked");
    }

    private void Attack2()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _attackMask);
        foreach (var enemy in enemies)
        {
            if (enemy.name == this.name)
            {

            }
            else
            {
                Damaged2();
            }
        }
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal2") * runSpeed;

        animator.SetFloat("Speed2", Mathf.Abs(horizontalMove));

        if (health2 <= 0)
        {
            animator.SetTrigger("isDead2");
            animator.SetBool("isDeadBool2", true);
            isDead2 = true;
            runSpeed = 0f;
        }

        if (Input.GetButtonDown("Jump2")) {
            jump = true;
        }

        if (Input.GetButtonDown("Fire2")) {
            animator.SetTrigger("isAttacking2");
        }

        if (Input.GetButtonDown("Crouch2")) {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch2")) {
            crouch = false;
        }
    }

    void FixedUpdate ()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
