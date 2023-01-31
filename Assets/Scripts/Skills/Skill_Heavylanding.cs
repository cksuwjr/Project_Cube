using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Heavylanding : Skill
{
    private void Awake()
    {
		inform.Add("[W] Heavy Landing ");
		inform.Add("������ ���������� ������ ������ �����Ͽ� �α� ������ ���̿� ����� �ִ� �� ����ü���� 8% ���� ���ظ� ������ ���� ������ 44% ��ŭ ���� ������ŵ�ϴ�.\n\n��Ÿ�� : 0.3��");
		inform.Add("������ ���������� ������ ������ �����Ͽ� �α� ������ ���̿� ����� �ִ� �� ����ü���� 16% ���� ���ظ� ������ ���� ������ 58% ��ŭ ���� ������ŵ�ϴ�.\n\n��Ÿ�� : 0.3��");
		inform.Add("������ ���������� ������ ������ �����Ͽ� �α� ������ ���̿� ����� �ִ� �� ����ü���� 24% ���� ���ظ� ������ ���� ������ 72% ��ŭ ���� ������ŵ�ϴ�.\n\n��Ÿ�� : 0.3��");
		inform.Add("������ ���������� ������ ������ �����Ͽ� �α� ������ ���̿� ����� �ִ� �� ����ü���� 32% ���� ���ظ� ������ ���� ������ 86% ��ŭ ���� ������ŵ�ϴ�.\n\n��Ÿ�� : 0.3��");
		inform.Add("������ ���������� ������ ������ �����Ͽ� �α� ������ ���̿� ����� �ִ� �� ����ü���� 40% ���� ���ظ� ������ ���� ������ 100% ��ŭ ���� ������ŵ�ϴ�.\n\n��Ÿ�� : 0.3��");
    }
    protected override IEnumerator Cast_()
	{
		if (!controller.isGround)
		{
			float nowy = transform.position.y;
			GetComponent<Rigidbody>().MovePosition(new Vector3(transform.position.x, 1, transform.position.z));
			
			yield return new WaitForSeconds(0.05f);
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			float basic_Attack_damage = 0;
			float Attack_coefficient = 0.04f * skill_Level;
			if(((nowy - 1)/ 2) <= 2)
				Attack_coefficient *= ((nowy - 1) / 2);
            else
				Attack_coefficient *= 2;
			
			float Airbornforce = (30 + 14 * skill_Level) * nowy;

			controller.Attack(new Vector3(15, 1, 15), basic_Attack_damage + (GetComponent<Status>().Hp * Attack_coefficient), "Middle", 0,0,"AirBorned", Airbornforce, "Yes");

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
