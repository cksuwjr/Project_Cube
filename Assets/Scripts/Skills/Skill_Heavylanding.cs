using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Heavylanding : Skill
{
    private void Awake()
    {
		inform.Add("[W] Heavy Landing ");
		inform.Add("점프 후 사용가능한 스킬이며 시전시 빠르게 착지하여 인근 적에게 내 현재체력의 4% 피해를 입히고 현재 높이의 30% 만큼 띄우며 기절시킵니다.\n\n쿨타임 : 0.3초");
		inform.Add("점프 후 사용가능한 스킬이며 시전시 빠르게 착지하여 인근 적에게 내 현재체력의 8% 피해를 입히고 현재 높이의 60% 만큼 띄우며 기절시킵니다.\n\n쿨타임 : 0.3초");
		inform.Add("점프 후 사용가능한 스킬이며 시전시 빠르게 착지하여 인근 적에게 내 현재체력의 12% 피해를 입히고 현재 높이의 90% 만큼 띄우며 기절시킵니다.\n\n쿨타임 : 0.3초");
		inform.Add("점프 후 사용가능한 스킬이며 시전시 빠르게 착지하여 인근 적에게 내 현재체력의 16% 피해를 입히고 현재 높이의 120% 만큼 띄우며 기절시킵니다.\n\n쿨타임 : 0.3초");
		inform.Add("점프 후 사용가능한 스킬이며 시전시 빠르게 착지하여 인근 적에게 내 현재체력의 20% 피해를 입히고 현재 높이의 150% 만큼 띄우며 기절시킵니다.\n\n쿨타임 : 0.3초");
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
