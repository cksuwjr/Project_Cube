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
	public bool isGround;            // Whether or not the player is grounded.
	private Rigidbody rb ;

	public Vector3 direction = Vector3.zero;
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

	[SerializeField] private GameObject RecentAttacker;

	// Skill
	[SerializeField] public Skill Skill_Q;
	[SerializeField] public Skill Skill_W;
	[SerializeField] public Skill Skill_E;
	[SerializeField] public Skill Skill_R;

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


	// ============================ 움직임 ===============================
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

	// ============================ 공격 범위 생성 및 적용 ===============================
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
							//Debug.Log(Deffender.name + "에어본당함!");
						}
					}
                }
                else
                    Debug.Log("공격대상이 Hit 컴포넌트를 소유하지 않았습니다.");
		}
	}
	// ============================ 스킬 시전 시스템 ===============================

	public void Q() { Skill(Skill_Q); }
	public void W() { Skill(Skill_W); }
	public void E() { Skill(Skill_E); }
	public void R() { Skill(Skill_R, Skill_E); }
	
	public void Skill(Skill skill = null, Skill Except = null)
    {
		if (Except != Skill_Q) Skill_Q.StopCast();
		if (Except != Skill_W) Skill_W.StopCast();
		if (Except != Skill_E) Skill_E.StopCast();
		if (Except != Skill_R) Skill_R.StopCast();

		if (skill != null) skill.Cast();
	}

	// ============================ 스탯 또는 버프 적용 ===============================

	public void PowerUp(float ad)
    {
		GetComponent<Status>().AttackPower += ad;
    }

	// ============================ 스탯(HP, 죽음) 관련 ===============================

	public void GetDamage(float damage, GameObject fromwho = null)
    {
		Status mystat = GetComponent<Status>();

		mystat.Hp -= damage;
		GetComponent<DamageSpawner>().Spawn(-damage);

		RecentAttacker = fromwho;

		if (mystat.Hp < 0)
			Die();
	}
	public void GetHeal(float heal)
    {
		Status mystat = GetComponent<Status>();

		mystat.Hp += heal;
		GetComponent<DamageSpawner>().Spawn(heal);

		if (mystat.Hp > mystat.MaxHp) // 체력 제한
			mystat.Hp = mystat.MaxHp;
	}
	public void Die()
    {
		if (myui != null)
		{
			for(int i = 0; i < myui.transform.childCount; i++)
				myui.transform.GetChild(i).gameObject.SetActive(false);
			myui.PopupDieUI();
		}
		if (RecentAttacker)
		{
			RecentAttacker.GetComponent<CubeController>().GetHeal(50);
			RecentAttacker.GetComponent<CubeController>().PowerUp(3);
		}
		Destroy(gameObject);
    }
	
	// ============================ CC기 적용 ===============================

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
	
	// ============================= 특수 조건 =====================================
	public void SelfDistriction()
	{
		if (transform.position.y < -5)// 추락시
			Die();
	}

}
