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

	private Vector3 direction = Vector3.zero;
	private Vector3 m_Velocity = Vector3.zero;


	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	// Status
	


	// Attack Part

	[SerializeField] private LayerMask WhatIsEnemy;
	[SerializeField] private Transform AttackPos;

	// UI
	[SerializeField] private CubeUI myui;


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




		// self Distriction(제한조건)
		SelfDistriction();
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
		if (dirAngle == 0)
			direction = new Vector3(0, rb.velocity.y, 1);
		else if (dirAngle == 45)
			direction = new Vector3(1, rb.velocity.y, 1);
		else if (dirAngle == 90)
			direction = new Vector3(1, rb.velocity.y, 0);
		else if (dirAngle == 135)
			direction = new Vector3(1, rb.velocity.y, -1);
		else if (dirAngle == 180)
			direction = new Vector3(0, rb.velocity.y, -1);
		else if (dirAngle == 225)
			direction = new Vector3(-1, rb.velocity.y, -1);
		else if (dirAngle == 270)
			direction = new Vector3(-1, rb.velocity.y, 0);
		else if (dirAngle == 315)
			direction = new Vector3(-1, rb.velocity.y, 1);

		Vector3 targetVelocity = new Vector3(direction.x * speed * 10f, direction.y, direction.z * speed * 10f);
		// And then smoothing it out and applying it to the character
		if (rb.useGravity) // 중력 적용 안될때는 움직임 적용 안 하려고 넣음
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, MovementSmoothing);

		// If the player should jump...
		if (isGround && jump)
		{
			// Add a vertical force to the player.
			isGround = false;
			rb.AddForce(new Vector2(0f, JumpForce));
		}
	}
	public void Attack(Vector3 attacksize, float attackdamage = -1)
    {
		Collider[] colliders = Physics.OverlapBox(AttackPos.position, attacksize, transform.rotation, WhatIsEnemy);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].isTrigger) continue; // isTrigger가 true인 collider는 감지 제외(collider가 여럿인 개체의 중복적용 방지위한 임시방책)

			Attack attackComponent = null;
			if (GetComponent<Attack>()) attackComponent = GetComponent<Attack>();
			else if (GetComponent<EnemyAttack>()) attackComponent = GetComponent<EnemyAttack>();

			if (attackComponent == null) return;

            GameObject Deffender = colliders[i].gameObject;
            if (attackComponent.isAttackTarget(Deffender))
                if (Deffender.GetComponent<Hit>())
                {
                    Hit hit = Deffender.GetComponent<Hit>();
					float Damage;
					if (attackdamage == -1) 
						Damage = attackComponent.CalcDamage(gameObject, Deffender);
					else 
						Damage = attackdamage;

					if (hit.isHitTarget())
                        hit.OnHit(gameObject, Damage, false);
                }
                else
                    Debug.Log("공격대상이 Hit 컴포넌트를 소유하지 않았습니다.");
		}
	}
	public void Q()
    {
		Debug.Log("Q");
    }
	IEnumerator Cast_Q()
	{
		yield return null;
	}

	public void W()
	{
		Debug.Log("W");
	}
	IEnumerator Cast_W()
	{
		yield return null;
	}

	public void E()
	{
		StopCoroutine("Cast_E");
		StartCoroutine("Cast_E");
		Debug.Log("E");
	}
	IEnumerator Cast_E()
    {
		// 중력 및 충돌 차단
		rb.useGravity = false;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

		// 무적
		GetComponent<Hit>().isHittable = false;

		// 공격 및 이동
		Attack(new Vector3(1, 1, 8), 9999);

		direction = new Vector3(direction.x, 0, direction.z);
		rb.velocity = direction * 50f + new Vector3(0, rb.velocity.y, 0);

		Debug.Log(direction * 500f + new Vector3(0, rb.velocity.y, 0));
	
		yield return new WaitForSeconds(0.05f);

		// 중력 및 충돌 재활성화
		GetComponent<Hit>().isHittable = true;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
		
		rb.useGravity = true;
	}

	public void R()
	{
		Debug.Log("R");
	}
	IEnumerator Cast_R()
	{
		yield return null;
	}

	public void PowerUp(float ad)
    {
		GetComponent<Status>().AttackPower += ad;
    }
	public void GetDamage(float damage)
    {
		Status mystat = GetComponent<Status>();

		mystat.Hp -= damage;
		GetComponent<DamageSpawner>().Spawn(-damage);
		
		if (mystat.Hp < 0)
			Die();
		Debug.Log(gameObject.name + "가 " + damage + "의 피해를 입었습니다! 남은체력: " + mystat.Hp);
	}
	public void GetHeal(float heal)
    {
		Status mystat = GetComponent<Status>();

		mystat.Hp += heal;
		GetComponent<DamageSpawner>().Spawn(heal);

		if (mystat.Hp > mystat.MaxHp) // 체력 제한
			mystat.Hp = mystat.MaxHp;
		Debug.Log(gameObject.name + "가 " + heal + "의 체력을 회복하였습니다! 현재체력: " + mystat.Hp);
	}
	public void Die()
    {
		if (myui != null)
		{
			myui.gameObject.SetActive(false);
			myui.PopupDieUI();
		}
		if (GameObject.Find("Cube"))
		{
			GameObject.Find("Cube").GetComponent<CubeController>().GetHeal(100);
			GameObject.Find("Cube").GetComponent<CubeController>().PowerUp(10);
		}
		Destroy(gameObject);
    }
	public void SelfDistriction()
    {
		if (transform.position.y < -5)// 추락시
			Die();

		
	}
    private void OnDrawGizmos()
    {
		//Physics.BoxCast(AttackPos.position, new Vector3(1, 1, 1), new Vector3(0,0,1), out RaycastHit hit, transform.rotation, 10);
		Gizmos.color = Color.green;
		
		
		Gizmos.DrawCube(AttackPos.position, new Vector3(1 * direction.x, 1, 10 * direction.z));
	}
}
