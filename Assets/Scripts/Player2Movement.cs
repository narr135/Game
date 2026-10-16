using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


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
    [SerializeField]
    private float stunDuration2 = 0.5f;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    public static float health2;
    private GameObject enemyPlayer;
    public GameObject gameManager;

    void Start()
    {
        health2 = hp2.mHealth;
        enemyPlayer = GameObject.Find("Player1");
        gameManager = GameObject.Find("Game Manager");
    }

    public void Damaged2()
    {
        Player1Movement.health -= 30f;
        enemyAnimator.SetTrigger("isAttacked");
        enemyPlayer.GetComponent<Player1Movement>().enabled = false;
        enemyAnimator.SetFloat("Speed", 0f);
    }

    private void Attack2()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _attackMask);
        foreach (var enemy in enemies)
        {
            if (enemy.name == this.name) { }
            else
            {
                Damaged2();
            }
        }
    }

    private void stoppedAttacking2()
    {
        this.GetComponent<Player2Movement>().enabled = true;
    }

    private void stoppedDamaged2()
    {
        enemyPlayer.GetComponent<Player1Movement>().enabled = true;
    }

    void Update()
    {
        var input = GameInput.Instance;
        if (input == null) return;

        horizontalMove = input.Move2 * runSpeed;

        animator.SetFloat("Speed2", Mathf.Abs(horizontalMove));

        if (health2 <= 0)
        {
            Debug.Log("player2 dead");
            horizontalMove = 0;
            animator.SetTrigger("isDead2");
            animator.SetFloat("Speed2", 0f);
            animator.SetBool("isDeadBool2", true);
            gameManager.GetComponent<timer>().GameOver();
        }

        if (input.Jump2Pressed)
        {
            jump = true;
        }

        if (input.Fire2Pressed)
        {
            animator.SetTrigger("isAttacking2");
            this.GetComponent<Player2Movement>().enabled = false;
        }

        crouch = input.Crouch2Held;

        if (stunDuration2 <= 0)
        {
            stunDuration2 -= Time.deltaTime;
        }
        else
        {
            enemyPlayer.GetComponent<Player1Movement>().enabled = true;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}