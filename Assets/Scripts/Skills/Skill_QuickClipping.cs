using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_QuickClipping : Skill
{
	protected override IEnumerator Cast_()
    {
		Rigidbody rb = GetComponent<Rigidbody>();

		transform.rotation = Quaternion.Euler(Vector3.zero);
		// �߷� �� �浹 ����
		rb.useGravity = false;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

		// ����
		GetComponent<Hit>().isHittable = false;
		Vector3 nowPos = transform.position;


		controller.direction = new Vector3(controller.direction.x, 0, controller.direction.z);
		rb.velocity = controller.direction * 80f + new Vector3(0, rb.velocity.y, 0);



		yield return new WaitForSeconds(0.05f);
		Vector3 afterPos = transform.position;

		// ������ ������ �����ϴ� �� ����
		controller.Attack(new Vector3(1, 1, Vector3.Distance(nowPos, afterPos)), GetComponent<Status>().AttackPower * 1.25f + 20, "Back");

		// �߷� �� �浹 ��Ȱ��ȭ
		rb.useGravity = true;

		yield return new WaitForSeconds(0.15f);
		GetComponent<Hit>().isHittable = true;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

	}
}
