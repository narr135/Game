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
    private float stunDuration = 0.5f;
	private float horizontalMove = 0f;
	private bool jump = false;
	private bool crouch = false;
	public static float health;
	private GameObject enemyPlayer2;
	public GameObject gameManager;

	void Start()
	{
		health = hp1.mHealth;
		enemyPlayer2 = GameObject.Find("Player2");
		gameManager = GameObject.Find("Game Manager");
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
		Player2Movement.health2 -= 30f;
		enemyAnimator2.SetTrigger("isAttacked2");
		enemyPlayer2.GetComponent<Player2Movement>().enabled = false;
		enemyAnimator2.SetFloat("Speed2", 0f);
		for (float i = 0; i < stunDuration; i = i + 1f)
		{
			if (stunDuration <= 0)
			{
				enemyPlayer2.GetComponent<Player2Movement>().enabled = true;
			}
			stunDuration -= Time.deltaTime;
		}
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

	private void stoppedAttacking()
	{
		this.GetComponent<Player1Movement>().enabled = true;
	}

    private void stoppedDamaged()
    {
        enemyPlayer2.GetComponent<Player2Movement>().enabled = true;
    }

    void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (health <= 0)
		{
            Debug.Log("player dead");
            horizontalMove = 0;
			animator.SetFloat("Speed", 0f);
			animator.SetTrigger("isDead");
			animator.SetBool("isDeadBool", true);
            gameManager.GetComponent<timer>().GameOver();
		}

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

		if (Input.GetButtonDown("Fire1"))
		{
			animator.SetTrigger("isAttacking");
			this.GetComponent<Player1Movement>().enabled = false;
		}

		if (Input.GetButtonDown("Crouch")) {
			crouch = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}
	}

	void FixedUpdate()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
