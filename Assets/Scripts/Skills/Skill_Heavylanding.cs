using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Heavylanding : Skill
{
    private void Awake()
    {
		inform.Add("[W] Heavy Landing ");
		inform.Add("점프가 가능해지며 시전시 빠르게 착지하여 인근 적에게 높이에 비례해 최대 내 현재체력의 8% 까지 피해를 입히고 현재 높이의 44% 만큼 띄우며 기절시킵니다.\n\n쿨타임 : 0.3초");
		inform.Add("점프가 가능해지며 시전시 빠르게 착지하여 인근 적에게 높이에 비례해 최대 내 현재체력의 16% 까지 피해를 입히고 현재 높이의 58% 만큼 띄우며 기절시킵니다.\n\n쿨타임 : 0.3초");
		inform.Add("점프가 가능해지며 시전시 빠르게 착지하여 인근 적에게 높이에 비례해 최대 내 현재체력의 24% 까지 피해를 입히고 현재 높이의 72% 만큼 띄우며 기절시킵니다.\n\n쿨타임 : 0.3초");
		inform.Add("점프가 가능해지며 시전시 빠르게 착지하여 인근 적에게 높이에 비례해 최대 내 현재체력의 32% 까지 피해를 입히고 현재 높이의 86% 만큼 띄우며 기절시킵니다.\n\n쿨타임 : 0.3초");
		inform.Add("점프가 가능해지며 시전시 빠르게 착지하여 인근 적에게 높이에 비례해 최대 내 현재체력의 40% 까지 피해를 입히고 현재 높이의 100% 만큼 띄우며 기절시킵니다.\n\n쿨타임 : 0.3초");
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
