using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{

    public CharacterController2 controller;
    public Animator animator;
    public float runSpeed = 20f;
    [Header("Player Properties")]
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private LayerMask _attackMask;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    int health;

    void Start()
    {
        health = maxHealth;
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
        health -= 90;
        animator.SetTrigger("isAttacked2");
    }

    public void Died2()
    {
        if (health <= 0)
        {
            animator.SetTrigger("isDead2");
        }
    }

    private void Attack2()
    {
        animator.SetTrigger("isAttacking2");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _attackMask);
        foreach (var enemy in enemies)
        {
            // if (enemy.TryGetComponent(out IDamageble hit))
            // {
            //     hit.Damage();
            // }
            if (enemy.name == this.name)
            {

            }
            else
            {
                Debug.Log("hit" + enemy.name);
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
            Attack2();
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
