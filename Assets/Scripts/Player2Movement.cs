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
	public float horizontalMove = 0f;
	private bool jump = false;
	private bool crouch = false;
	public static float health2;
	public float stunDuration2 = 0.5f;
	private GameObject enemyPlayer;

	void Start()
	{
		health2 = hp2.mHealth;
		enemyPlayer = GameObject.Find("Player1");
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
		enemyPlayer.GetComponent<Player1Movement>().enabled = false;
		enemyAnimator.SetFloat("Speed", 0f);
		for (float i = 0; i < stunDuration2; i = i + 1f)
		{
			if (stunDuration2 <= 0)
			{
				enemyPlayer.GetComponent<Player2Movement>().enabled = true;
			}
			stunDuration2 -= Time.deltaTime;
		}
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
		horizontalMove = Input.GetAxisRaw("Horizontal2") * runSpeed;

		animator.SetFloat("Speed2", Mathf.Abs(horizontalMove));

		if (health2 <= 0)
		{
			horizontalMove = 0;
			FindObjectOfType<timer>().gameEnded = true;
			animator.SetTrigger("isDead2");
			animator.SetFloat("Speed2", 0f);
			animator.SetBool("isDeadBool2", true);
		}

		if (Input.GetButtonDown("Jump2")) {
			jump = true;
		}

		if (Input.GetButtonDown("Fire2")) {
			animator.SetTrigger("isAttacking2");
			this.GetComponent<Player2Movement>().enabled = false;
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
