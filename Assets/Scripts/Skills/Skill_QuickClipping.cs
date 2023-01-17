using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_QuickClipping : Skill
{
	private void Awake()
	{
		inform.Add("[E] Quick Clipping ");
		inform.Add("�ش� �������� �����Ÿ� �����ϸ� ���� �Ÿ� ������ ���� ��� 40 + 75% ���ݷ��� ���ظ� �Խ��ϴ�.\n\n��Ÿ�� : 2��");
		inform.Add("�ش� �������� �����Ÿ� �����ϸ� ���� �Ÿ� ������ ���� ��� 55 + 85% ���ݷ��� ���ظ� �Խ��ϴ�.\n\n��Ÿ�� : 2��");
		inform.Add("�ش� �������� �����Ÿ� �����ϸ� ���� �Ÿ� ������ ���� ��� 70 + 95% ���ݷ��� ���ظ� �Խ��ϴ�.\n\n��Ÿ�� : 2��");
		inform.Add("�ش� �������� �����Ÿ� �����ϸ� ���� �Ÿ� ������ ���� ��� 85 + 105% ���ݷ��� ���ظ� �Խ��ϴ�.\n\n��Ÿ�� : 2��");
		inform.Add("�ش� �������� �����Ÿ� �����ϸ� ���� �Ÿ� ������ ���� ��� 100 + 115% ���ݷ��� ���ظ� �Խ��ϴ�.\n\n��Ÿ�� : 2��");
	}
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
		float basic_Attack_Damage = 25 + 15 * skill_Level;
		float Attack_coefficient = 0.65f + (0.1f * skill_Level);

		controller.Attack(new Vector3(1, 1, Vector3.Distance(nowPos, afterPos)), basic_Attack_Damage + GetComponent<Status>().AttackPower * Attack_coefficient, "Back");

		// �߷� �� �浹 ��Ȱ��ȭ
		rb.useGravity = true;

		yield return new WaitForSeconds(0.15f);
		GetComponent<Hit>().isHittable = true;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

	}
}
