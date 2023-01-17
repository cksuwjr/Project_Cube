using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RotaryLaunch : Skill
{
	[SerializeField] private GameObject Bullet;

	private void Awake()
	{
		inform.Add("[R] Rotary Launcher ");
		inform.Add("인근 모든 방향에 난사하여 범위 내의 적에게 5% 공격력의 피해를 주고 탄이 적중한 적은 14 ~ 33% 공격력의 피해를 입습니다. 0.05초마다 발사하며 2.5초간 지속됩니다.\n\n쿨타임 : 3초");
		inform.Add("인근 모든 방향에 난사하여 범위 내의 적에게 10% 공격력의 피해를 주고 탄이 적중한 적은 14.5 ~ 33.5% 공격력의 피해를 입습니다. 0.05초마다 발사하며 2.5초간 지속됩니다.\n\n쿨타임 : 3초");
		inform.Add("인근 모든 방향에 난사하여 범위 내의 적에게 15% 공격력의 피해를 주고 탄이 적중한 적은 15 ~ 34% 공격력의 피해를 입습니다. 0.05초마다 발사하며 2.5초간 지속됩니다.\n\n쿨타임 : 3초");
		inform.Add("인근 모든 방향에 난사하여 범위 내의 적에게 20% 공격력의 피해를 주고 탄이 적중한 적은 15.5 ~ 34.5% 공격력의 피해를 입습니다. 0.05초마다 발사하며 2.5초간 지속됩니다.\n\n쿨타임 : 3초");
		inform.Add("인근 모든 방향에 난사하여 범위 내의 적에게 25% 공격력의 피해를 주고 탄이 적중한 적은 16 ~ 35% 공격력의 피해를 입습니다. 0.05초마다 발사하며 2.5초간 지속됩니다.\n\n쿨타임 : 3초");
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
			if (count++ > 2)
			{
				float basic_Attack_damage = 0;
				float Attack_coefficient = (0.05f * skill_Level);
				controller.Attack(new Vector3(6f, 1, 6f), (basic_Attack_damage + (status.AttackPower * Attack_coefficient)), "Middle");
				count = 0;
			}
			transform.Rotate(new Vector3(0, 225, 0));
			timer += 0.05f;
			yield return new WaitForSeconds(0.05f);
			float basic_Attack_damage2 = 1;
			float min_Attack_coefficient = 0.135f + (0.05f * skill_Level);
			float max_Attack_coefficient = 0.325f + (0.05f * skill_Level);
			Instantiate(Bullet, transform.position, transform.rotation).GetComponent<Bullet>().Tang(gameObject, basic_Attack_damage2 + (status.AttackPower * (Random.Range(min_Attack_coefficient, max_Attack_coefficient))), 45f, 0.15f);
		}
		transform.rotation = Quaternion.Euler(Vector3.zero);
	}
}
