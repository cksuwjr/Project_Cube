using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeController : MonoBehaviour
{

	// Move Part

	[SerializeField] private float JumpForce = 400f;                          // Amount of force added when the player jumps.

	[Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;

	[SerializeField] private LayerMask WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform GroundCheck;                           // A position marking where to check if the player is grounded.

	const float GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool isGround;            // Whether or not the player is grounded.
	private Rigidbody rb ;
	private Vector3 m_Velocity = Vector3.zero;


	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;


	// Attack Part

	[SerializeField] private LayerMask WhatIsEnemy;
	[SerializeField] private Transform AttackPos;


	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }


	private void Awake()
	{
		// Move Part	
		rb = GetComponent<Rigidbody>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{
		// Move Part
		bool wasGrounded = isGround;
		isGround = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider[] colliders = Physics.OverlapSphere(GroundCheck.position, GroundedRadius, WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGround = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

		
	}


	public void Move(float dirAngle, float speed, bool jump)
	{
		//only control the player if grounded or airControl is turned on
		if (isGround)
		{
			
		}
		// 큐브 회전
		rb.rotation = Quaternion.Euler(0, dirAngle, 0);

		// 각도에 따라 움직임xz 구해서 가기
		Vector3 dir = Vector3.zero;
		if (dirAngle == 0)
			dir = new Vector3(0, rb.velocity.y, speed * 10f);
		else if (dirAngle == 45)
			dir = new Vector3(speed * 10f, rb.velocity.y, speed * 10f);
		else if (dirAngle == 90)
			dir = new Vector3(speed * 10f, rb.velocity.y, 0);
		else if (dirAngle == 135)
			dir = new Vector3(speed * 10f, rb.velocity.y, -speed * 10f);
		else if (dirAngle == 180)
			dir = new Vector3(0, rb.velocity.y, -speed * 10f);
		else if (dirAngle == 225)
			dir = new Vector3(-speed * 10f, rb.velocity.y, -speed * 10f);
		else if (dirAngle == 270)
			dir = new Vector3(-speed * 10f, rb.velocity.y, 0);
		else if (dirAngle == 315)
			dir = new Vector3(-speed * 10f, rb.velocity.y, speed * 10f);

		Vector3 targetVelocity = dir;
															   // And then smoothing it out and applying it to the character
		rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, MovementSmoothing);

		// If the player should jump...
		if (isGround && jump)
		{
			// Add a vertical force to the player.
			isGround = false;
			rb.AddForce(new Vector2(0f, JumpForce));
		}
	}
	public void Attack(Vector3 attacksize)
    {
		Collider[] colliders = Physics.OverlapBox(AttackPos.position, attacksize, Quaternion.identity, WhatIsEnemy);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (gameObject.name == "Cube")
			{
				Attack attackComponent = GetComponent<Attack>();
				GameObject Deffender = colliders[i].gameObject;
				if (attackComponent.isAttackTarget(Deffender))
					if (Deffender.GetComponent<Hit>())
					{
						Hit hit = Deffender.GetComponent<Hit>();
						if (hit.isHitTarget())
							hit.OnHit(gameObject, attackComponent.CalcDamage(gameObject, Deffender), false);
					}
					else
						Debug.Log("공격대상이 Hit 컴포넌트를 소유하지 않았습니다.");
			}else if (gameObject.name == "Enemy")
            {
				EnemyAttack attackComponent = GetComponent<EnemyAttack>();
				GameObject Deffender = colliders[i].gameObject;
				if (attackComponent.isAttackTarget(Deffender))
					if (Deffender.GetComponent<Hit>())
					{
						Hit hit = Deffender.GetComponent<Hit>();
						if (hit.isHitTarget())
							hit.OnHit(gameObject, attackComponent.CalcDamage(gameObject, Deffender), false);
					}
					else
						Debug.Log("공격대상이 Hit 컴포넌트를 소유하지 않았습니다.");
			}
		}


		//if (AttackType == "BasicAttack")
		//	GetComponent<Attack>().BasicAttack();
	}
}
