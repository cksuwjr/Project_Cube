using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Heavylanding : Skill
{
    private void Awake()
    {
		inform.Add("[W] Heavy Landing ");
		inform.Add("���� �� ��밡���� ��ų�̸� ������ ������ �����Ͽ� �α� ������ �� ����ü���� 4% ���ظ� ������ ���� ������ 30% ��ŭ ���� ������ŵ�ϴ�.\n\n��Ÿ�� : 0.3��");
		inform.Add("���� �� ��밡���� ��ų�̸� ������ ������ �����Ͽ� �α� ������ �� ����ü���� 8% ���ظ� ������ ���� ������ 60% ��ŭ ���� ������ŵ�ϴ�.\n\n��Ÿ�� : 0.3��");
		inform.Add("���� �� ��밡���� ��ų�̸� ������ ������ �����Ͽ� �α� ������ �� ����ü���� 12% ���ظ� ������ ���� ������ 90% ��ŭ ���� ������ŵ�ϴ�.\n\n��Ÿ�� : 0.3��");
		inform.Add("���� �� ��밡���� ��ų�̸� ������ ������ �����Ͽ� �α� ������ �� ����ü���� 16% ���ظ� ������ ���� ������ 120% ��ŭ ���� ������ŵ�ϴ�.\n\n��Ÿ�� : 0.3��");
		inform.Add("���� �� ��밡���� ��ų�̸� ������ ������ �����Ͽ� �α� ������ �� ����ü���� 20% ���ظ� ������ ���� ������ 150% ��ŭ ���� ������ŵ�ϴ�.\n\n��Ÿ�� : 0.3��");
    }
    protected override IEnumerator Cast_()
	{
		if (!controller.isGround)
		{
			float nowy = transform.position.y;
			GetComponent<Rigidbody>().MovePosition(new Vector3(transform.position.x, 1, transform.position.z));
			yield return new WaitForSeconds(0.05f);
			float basic_Attack_damage = 0;
			float Attack_coefficient = 0.04f * skill_Level;
			float Airbornforce = (30 * skill_Level) * nowy;

			controller.Attack(new Vector3(15, 1, 15), basic_Attack_damage + (GetComponent<Status>().Hp * Attack_coefficient), "Middle", "AirBorned", Airbornforce, "Yes");

			/*
			GameObject[] enemys;
			if (gameObject.layer == LayerMask.NameToLayer("Player"))
				enemys = GameObject.FindGameObjectsWithTag("Enemy");
			else
				enemys = GameObject.FindGameObjectsWithTag("Player");
			for (int i = 0; i < enemys.Length; i++)
			{
				CubeController enemycontroller = enemys[i].GetComponent<CubeController>();
				if (enemycontroller.isGround)
				{
					enemycontroller.GetComponent<Rigidbody>().velocity = Vector3.zero;
					enemycontroller.AirBorned(Airbornforce);
					enemycontroller.StartCoroutine(enemycontroller.CC(0.004f * Airbornforce));
				}
			}
			*/
		}
		yield return null;
	}
}
