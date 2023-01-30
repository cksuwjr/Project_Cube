using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RotaryLaunch : Skill
{
	[SerializeField] private GameObject Bullet;

	private void Awake()
	{
		inform.Add("[R] Rotary Launcher ");
		inform.Add("�α� ��� ���⿡ �����Ͽ� ���� ���� ������ 5 + 5% ���ݷ��� ���ظ� �ְ� ź�� ������ ���� 5 ~ 25% ���ݷ��� ���ظ� �Խ��ϴ�. Q��ų������ ���� ġ��Ÿ�� ����Ǹ� óġ�� ��Ÿ���� 0.5�� �����մϴ�. 0.05�ʸ��� �߻��ϸ� 2.5�ʰ� ���ӵ˴ϴ�.\n\n��Ÿ�� : 10��");
		inform.Add("�α� ��� ���⿡ �����Ͽ� ���� ���� ������ 10 + 10% ���ݷ��� ���ظ� �ְ� ź�� ������ ���� 10 ~ 50% ���ݷ��� ���ظ� �Խ��ϴ�. Q��ų������ ���� ġ��Ÿ�� ����Ǹ� óġ�� ��Ÿ���� 1�ʾ� �����մϴ�. 0.05�ʸ��� �߻��ϸ� 2.5�ʰ� ���ӵ˴ϴ�.\n\n��Ÿ�� : 10��");
		inform.Add("�α� ��� ���⿡ �����Ͽ� ���� ���� ������ 15 + 15% ���ݷ��� ���ظ� �ְ� ź�� ������ ���� 15 ~ 75% ���ݷ��� ���ظ� �Խ��ϴ�. Q��ų������ ���� ġ��Ÿ�� ����Ǹ� óġ�� ��Ÿ���� 1.5�ʾ� �����մϴ�. 0.05�ʸ��� �߻��ϸ� 2.5�ʰ� ���ӵ˴ϴ�.\n\n��Ÿ�� : 10��");
		inform.Add("�α� ��� ���⿡ �����Ͽ� ���� ���� ������ 20 + 20% ���ݷ��� ���ظ� �ְ� ź�� ������ ���� 20 ~ 100% ���ݷ��� ���ظ� �Խ��ϴ�. Q��ų������ ���� ġ��Ÿ�� ����Ǹ� óġ�� ��Ÿ���� 2�ʾ� �����մϴ�. 0.05�ʸ��� �߻��ϸ� 2.5�ʰ� ���ӵ˴ϴ�.\n\n��Ÿ�� : 10��");
		inform.Add("�α� ��� ���⿡ �����Ͽ� ���� ���� ������ 25 + 25% ���ݷ��� ���ظ� �ְ� ź�� ������ ���� 25 ~ 125% ���ݷ��� ���ظ� �Խ��ϴ�. Q��ų������ ���� ġ��Ÿ�� ����Ǹ� óġ�� ��Ÿ���� 2.5�ʾ� �����մϴ�. 0.05�ʸ��� �߻��ϸ� 2.5�ʰ� ���ӵ˴ϴ�.\n\n��Ÿ�� : 10��");
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
