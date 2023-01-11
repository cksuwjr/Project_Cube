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

	// CC
	
	public bool IsBinded = false;
	public bool IsActable = true;


	// Attack Part

	[SerializeField] private LayerMask WhatIsEnemy;
	[SerializeField] private Transform AttackPos;

	[SerializeField] private GameObject Bullet;

	[SerializeField] private GameObject RecentAttacker;

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
		if(speed != 0)
			rb.rotation = Quaternion.Euler(0, dirAngle, 0);

		// 각도에 따라 움직임xz 구해서 가기
		if (dirAngle == 0)        direction = new Vector3(0, rb.velocity.y, 1);
		else if (dirAngle == 45)  direction = new Vector3(1, rb.velocity.y, 1);
		else if (dirAngle == 90)  direction = new Vector3(1, rb.velocity.y, 0);
		else if (dirAngle == 135) direction = new Vector3(1, rb.velocity.y, -1);
		else if (dirAngle == 180) direction = new Vector3(0, rb.velocity.y, -1);
		else if (dirAngle == 225) direction = new Vector3(-1, rb.velocity.y, -1);
		else if (dirAngle == 270) direction = new Vector3(-1, rb.velocity.y, 0);
		else if (dirAngle == 315) direction = new Vector3(-1, rb.velocity.y, 1);

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
	public void Attack(Vector3 attacksize, float attackdamage = -1, string isFront = "Front", string CC = "None", float howmuch = 0) // 공격 크기 (1, 1, 길이), 데미지, 앞이냐 뒤냐(ex 지나간자리)
    {
		Vector3 attackPos;
		if (isFront == "Front")
			attackPos = AttackPos.position + new Vector3(((attacksize.z - 1) / 2 * direction.x), 0, ((attacksize.z - 1) / 2) * direction.z);
		else if(isFront == "Back")
			attackPos = AttackPos.position - new Vector3(((attacksize.z - 1) / 2 * direction.x), 0, ((attacksize.z - 1) / 2) * direction.z);
        else // Middle
			attackPos = AttackPos.position;
		//attackPos = isFront ? AttackPos.position + new Vector3(((attacksize.z - 1) / 2 * direction.x), 0, ((attacksize.z - 1) / 2) * direction.z) : AttackPos.position - new Vector3(((attacksize.z - 1) / 2 * direction.x), 0, ((attacksize.z - 1) / 2) * direction.z);
		Collider[] colliders = Physics.OverlapBox(attackPos, attacksize / 2, transform.rotation, WhatIsEnemy);
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
					{
						hit.OnHit(gameObject, Damage, false);
						if (CC == "AirBorned")
						{
							Deffender.GetComponent<CubeController>().AirBorned(howmuch);
							Debug.Log(Deffender.name + "에어본당함!");
						}
					}
                }
                else
                    Debug.Log("공격대상이 Hit 컴포넌트를 소유하지 않았습니다.");
		}
	}
	public void Q() { Skill("Cast_Q"); }
	public void W() { Skill("Cast_W"); }
	public void E() { Skill("Cast_E"); }
	public void R() { Skill("Cast_R", "Cast_E"); }
	IEnumerator Cast_Q()
	{
		Instantiate(Bullet, transform.position, transform.rotation).GetComponent<Bullet>().Tang(gameObject, GetComponent<Status>().AttackPower * 0.5f, 30, 0.8f);
		
		yield return null;
	}

	IEnumerator Cast_W()
	{
		if (!isGround)
        {
			float Airbornforce = 100 * transform.position.y;
			rb.MovePosition(new Vector3(transform.position.x, 1, transform.position.z));
			GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
			for (int i = 0; i < enemys.Length; i++)
			{
				CubeController enemycontroller = enemys[i].GetComponent<CubeController>();
				if (enemycontroller.isGround)
				{
					enemycontroller.AirBorned(Airbornforce);
					enemycontroller.GetDamage(1, gameObject);
				}
			}
		}
		

		yield return null;
	}

	
	IEnumerator Cast_E()
    {
		transform.rotation = Quaternion.Euler(Vector3.zero);
		// 중력 및 충돌 차단
		rb.useGravity = false;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

		// 무적
		GetComponent<Hit>().isHittable = false;
		Vector3 nowPos = transform.position;
		

		direction = new Vector3(direction.x, 0, direction.z);
		rb.velocity = direction * 80f + new Vector3(0, rb.velocity.y, 0);

		
	
		yield return new WaitForSeconds(0.05f);
		Vector3 afterPos = transform.position;

		// 지나간 공간에 존재하는 적 공격
		Attack(new Vector3(1, 1, Vector3.Distance(nowPos, afterPos)), GetComponent<Status>().AttackPower * 1.25f + 20, "Back", "AirBorned", 500f);

		// 도착후 인근 띄우며 공격
		//Attack(new Vector3(3, 1, 3), GetComponent<Status>().AttackPower, "Middle", "AirBorned", 500f);

		// 중력 및 충돌 재활성화
		rb.useGravity = true;

		yield return new WaitForSeconds(0.15f);
		GetComponent<Hit>().isHittable = true;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
		
	}

	
	IEnumerator Cast_R()
	{
		float timer = 0f;
		int count = 0;
		while(timer < 2.5f)
        {
			if (count++ > 2)
			{
				Attack(new Vector3(6f, 1, 6f), 25f, "Middle");
				count = 0;
			}
			transform.Rotate(new Vector3(0, 225, 0));
			timer += 0.05f;
			yield return new WaitForSeconds(0.05f);
			Instantiate(Bullet, transform.position, transform.rotation).GetComponent<Bullet>().Tang(gameObject, GetComponent<Status>().AttackPower * (Random.Range(0.185f, 0.325f)), 45f, 0.15f);
		}
		transform.rotation = Quaternion.Euler(Vector3.zero);
	}
	public void Skill(string Cast_What = null, string Except = null)
    {
		if (Except != "Cast_Q") StopCoroutine("Cast_Q");
		if (Except != "Cast_W") StopCoroutine("Cast_W");
		if (Except != "Cast_E") StopCoroutine("Cast_E");
		if (Except != "Cast_R") StopCoroutine("Cast_R");

		if (Cast_What != null) StartCoroutine(Cast_What);
	}

	public void PowerUp(float ad)
    {
		GetComponent<Status>().AttackPower += ad;
    }
	public void GetDamage(float damage, GameObject fromwho = null)
    {
		Status mystat = GetComponent<Status>();

		mystat.Hp -= damage;
		GetComponent<DamageSpawner>().Spawn(-damage);

		RecentAttacker = fromwho;

		if (mystat.Hp < 0)
			Die();
		//Debug.Log(gameObject.name + "가 " + damage + "의 피해를 입었습니다! 남은체력: " + mystat.Hp);
	}
	public void GetHeal(float heal)
    {
		Status mystat = GetComponent<Status>();

		mystat.Hp += heal;
		GetComponent<DamageSpawner>().Spawn(heal);

		if (mystat.Hp > mystat.MaxHp) // 체력 제한
			mystat.Hp = mystat.MaxHp;
		//Debug.Log(gameObject.name + "가 " + heal + "의 체력을 회복하였습니다! 현재체력: " + mystat.Hp);
	}
	public void Die()
    {
		if (myui != null)
		{
			myui.gameObject.SetActive(false);
			myui.PopupDieUI();
		}
		if (RecentAttacker)
		{
			RecentAttacker.GetComponent<CubeController>().GetHeal(50);
			RecentAttacker.GetComponent<CubeController>().PowerUp(3);
		}
		Destroy(gameObject);
    }
	public void SelfDistriction()
    {
		if (transform.position.y < -5)// 추락시
			Die();

		
	}
	
	// CC기 적용

	IEnumerator CC(float time)
	{
		IsBinded = true;
		yield return new WaitForSeconds(time);
		IsBinded = false;
	}

	IEnumerator ActMumchit(float time)
	{
		IsActable = false;
		yield return new WaitForSeconds(time);
		IsActable = true;
	}

	public void AirBorned(float howmuch)
    {
		rb.AddForce(new Vector2(0, howmuch));
    }
	public void KnockBack(GameObject fromwho, float howmuch)
    {
		StartCoroutine(CC(0.3f));

		Vector3 dir = Vector3.zero;
		try{ dir = (fromwho.transform.position - transform.position).normalized;} catch { }
		transform.rotation = Quaternion.LookRotation(dir);
		rb.velocity = new Vector3(-dir.x * howmuch, rb.velocity.y, -dir.z * howmuch);
    }

 //   private void OnDrawGizmos()
 //   {
	//	Gizmos.color = Color.green;
	//	Gizmos.DrawCube(AttackPos.position, new Vector3(1 * direction.x, 1, 10 * direction.z));
	//}
}
