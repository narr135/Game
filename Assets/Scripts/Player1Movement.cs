using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Movement : MonoBehaviour
{

    public CharacterController1 controller;
    public Animator animator;
    public Animator enemyAnimator2;
    [Header("Player Properties")]
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private float _attackRange = 1f;
    [SerializeField]
    private LayerMask _attackMask;
    [SerializeField]
    private float runSpeed = 20f;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    public static bool isDead = false;
    public static int health;

    void Start()
    {
        health = maxHealth;
        // HealthBar.setMaxHealth(maxHealth);
    }

    // private void OnDrawGizmos()
    // {
    //     if (_attackPoint is null) {
    //         return;
    //     }

    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    // }

    public void Damaged()
    {
        Player2Movement.health2 -= 30;
        // HealthBar2.setHealth2(Player2Movement.health2);
        enemyAnimator2.SetTrigger("isAttacked2");
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _attackMask);
        foreach (var enemy in enemies)
        {
            if (enemy.name == this.name)
            {

            }
            else
            {
                Damaged();
            }
        }

    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (health <= 0)
        {
            animator.SetTrigger("isDead");
            animator.SetBool("isDeadBool", true);
            isDead = true;
            runSpeed = 0f;
        }

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        if (Input.GetButtonDown("Fire1")) {
            animator.SetTrigger("isAttacking");
        }

        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }
    }

    void FixedUpdate ()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
