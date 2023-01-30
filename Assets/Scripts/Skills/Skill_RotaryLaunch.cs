using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RotaryLaunch : Skill
{
	[SerializeField] private GameObject Bullet;

	private void Awake()
	{
		inform.Add("[R] Rotary Launcher ");
		inform.Add("인근 모든 방향에 난사하여 범위 내의 적에게 5 + 5% 공격력의 피해를 주고 탄이 적중한 적은 5 ~ 25% 공격력의 피해를 입습니다. Q스킬레벨에 따라 치명타가 적용되며 처치시 쿨타임이 0.5초 감소합니다. 0.05초마다 발사하며 2.5초간 지속됩니다.\n\n쿨타임 : 10초");
		inform.Add("인근 모든 방향에 난사하여 범위 내의 적에게 10 + 10% 공격력의 피해를 주고 탄이 적중한 적은 10 ~ 50% 공격력의 피해를 입습니다. Q스킬레벨에 따라 치명타가 적용되며 처치시 쿨타임이 1초씩 감소합니다. 0.05초마다 발사하며 2.5초간 지속됩니다.\n\n쿨타임 : 10초");
		inform.Add("인근 모든 방향에 난사하여 범위 내의 적에게 15 + 15% 공격력의 피해를 주고 탄이 적중한 적은 15 ~ 75% 공격력의 피해를 입습니다. Q스킬레벨에 따라 치명타가 적용되며 처치시 쿨타임이 1.5초씩 감소합니다. 0.05초마다 발사하며 2.5초간 지속됩니다.\n\n쿨타임 : 10초");
		inform.Add("인근 모든 방향에 난사하여 범위 내의 적에게 20 + 20% 공격력의 피해를 주고 탄이 적중한 적은 20 ~ 100% 공격력의 피해를 입습니다. Q스킬레벨에 따라 치명타가 적용되며 처치시 쿨타임이 2초씩 감소합니다. 0.05초마다 발사하며 2.5초간 지속됩니다.\n\n쿨타임 : 10초");
		inform.Add("인근 모든 방향에 난사하여 범위 내의 적에게 25 + 25% 공격력의 피해를 주고 탄이 적중한 적은 25 ~ 125% 공격력의 피해를 입습니다. Q스킬레벨에 따라 치명타가 적용되며 처치시 쿨타임이 2.5초씩 감소합니다. 0.05초마다 발사하며 2.5초간 지속됩니다.\n\n쿨타임 : 10초");
	}
	protected override IEnumerator Cast_()
	{
		float timer = 0f;
		int count = 0;
		Status status = GetComponent<Status>();

		while (timer < 2.5f)
		{
			if (GetComponent<PlayerMovement>() && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
				StopCoroutine("Cast_");

			float qSkillLevel = GetComponent<CubeController>().Skill_Q.skill_Level;
			float CriticalProbablity = -5 + 5 * qSkillLevel;
			float CriticalDamageMagnification = 0.7f + 0.3f * qSkillLevel;
			if (count++ > 2)
			{
				float basic_Attack_damage = 5 * skill_Level;
				float Attack_coefficient = (0.05f * skill_Level);
				controller.Attack(new Vector3(6f, 1, 6f), (basic_Attack_damage + (status.AttackPower * Attack_coefficient)), "Middle", CriticalProbablity, CriticalDamageMagnification);
				count = 0;
			}
			transform.Rotate(new Vector3(0, 225, 0));
			timer += 0.05f;
			yield return new WaitForSeconds(0.05f);
			float basic_Attack_damage2 = 0;
			float min_Attack_coefficient = 0.00f + (0.05f * skill_Level);
			float max_Attack_coefficient = 0.00f + (0.25f * skill_Level);


			Instantiate(Bullet, transform.position, transform.rotation).GetComponent<Bullet>().Tang(gameObject, basic_Attack_damage2 + (status.AttackPower * (Random.Range(min_Attack_coefficient, max_Attack_coefficient))), 50f, 0.15f, CriticalProbablity, CriticalDamageMagnification);
		}
		transform.rotation = Quaternion.Euler(Vector3.zero);
	}
}
