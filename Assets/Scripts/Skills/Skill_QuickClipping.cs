using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_QuickClipping : Skill
{
	private void Awake()
	{
		inform.Add("[E] Quick Clipping ");
		inform.Add("해당 방향으로 일정거리 돌진하며 돌진 거리 사이의 적은 모두 40 + 75% 공격력의 피해를 입습니다.\n\n쿨타임 : 2초");
		inform.Add("해당 방향으로 일정거리 돌진하며 돌진 거리 사이의 적은 모두 55 + 85% 공격력의 피해를 입습니다.\n\n쿨타임 : 2초");
		inform.Add("해당 방향으로 일정거리 돌진하며 돌진 거리 사이의 적은 모두 70 + 95% 공격력의 피해를 입습니다.\n\n쿨타임 : 2초");
		inform.Add("해당 방향으로 일정거리 돌진하며 돌진 거리 사이의 적은 모두 85 + 105% 공격력의 피해를 입습니다.\n\n쿨타임 : 2초");
		inform.Add("해당 방향으로 일정거리 돌진하며 돌진 거리 사이의 적은 모두 100 + 115% 공격력의 피해를 입습니다.\n\n쿨타임 : 2초");
	}
	protected override IEnumerator Cast_()
    {
		Rigidbody rb = GetComponent<Rigidbody>();

		transform.rotation = Quaternion.Euler(Vector3.zero);
		// 중력 및 충돌 차단
		rb.useGravity = false;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

		// 무적
		GetComponent<Hit>().isHittable = false;
		Vector3 nowPos = transform.position;


		controller.direction = new Vector3(controller.direction.x, 0, controller.direction.z);
		rb.velocity = controller.direction * 80f + new Vector3(0, rb.velocity.y, 0);



		yield return new WaitForSeconds(0.05f);
		Vector3 afterPos = transform.position;


		// 지나간 공간에 존재하는 적 공격
		float basic_Attack_Damage = 25 + 15 * skill_Level;
		float Attack_coefficient = 0.65f + (0.1f * skill_Level);

		controller.Attack(new Vector3(1, 1, Vector3.Distance(nowPos, afterPos)), basic_Attack_Damage + GetComponent<Status>().AttackPower * Attack_coefficient, "Back");

		// 중력 및 충돌 재활성화
		rb.useGravity = true;

		yield return new WaitForSeconds(0.15f);
		GetComponent<Hit>().isHittable = true;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

	}
}
