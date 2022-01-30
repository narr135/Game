using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{

    public CharacterController1 controller;
    public Animator animator;
    public float runSpeed = 20f;
    [Header("Player Properties")]
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private float _attackRange = 1f;
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

    public void Damaged()
    {
        health -= 90;
        animator.SetTrigger("isAttacked");
    }

    public void Died()
    {
        if (health <= 0)
        {
            animator.SetTrigger("isDead");
        }
    }

    private void Attack()
    {
        animator.SetTrigger("isAttacking");
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
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        if (Input.GetButtonDown("Fire1")) {
            Attack();
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
