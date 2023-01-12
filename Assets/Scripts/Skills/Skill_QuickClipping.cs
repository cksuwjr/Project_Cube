using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_QuickClipping : Skill
{
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
		controller.Attack(new Vector3(1, 1, Vector3.Distance(nowPos, afterPos)), GetComponent<Status>().AttackPower * 1.25f + 20, "Back");

		// 중력 및 충돌 재활성화
		rb.useGravity = true;

		yield return new WaitForSeconds(0.15f);
		GetComponent<Hit>().isHittable = true;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

	}
}
