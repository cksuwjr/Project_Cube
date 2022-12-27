using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeController : MonoBehaviour
{
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

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }


	private void Awake()
	{
		rb = GetComponent<Rigidbody>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{
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


	private void Flip()
	{
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
