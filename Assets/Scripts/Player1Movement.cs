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
	private Transform _attackPoint;
	[SerializeField]
	private float _attackRange = 1f;
	[SerializeField]
	private LayerMask _attackMask;
	[SerializeField]
	private float runSpeed = 20f;
	[SerializeField]
	private float horizontalMove = 0f;
	private bool jump = false;
	private bool crouch = false;
	public static float health;

	void Start()
	{
		health = hp1.mHealth;
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
			FindObjectOfType<timer>().gameEnded = true;
			animator.SetTrigger("isDead");
			animator.SetBool("isDeadBool", true);
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
