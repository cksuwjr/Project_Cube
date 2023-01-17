using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RotaryLaunch : Skill
{
	[SerializeField] private GameObject Bullet;

	private void Awake()
	{
		inform.Add("[R] Rotary Launcher ");
		inform.Add("�α� ��� ���⿡ �����Ͽ� ���� ���� ������ 5% ���ݷ��� ���ظ� �ְ� ź�� ������ ���� 14 ~ 33% ���ݷ��� ���ظ� �Խ��ϴ�. 0.05�ʸ��� �߻��ϸ� 2.5�ʰ� ���ӵ˴ϴ�.\n\n��Ÿ�� : 3��");
		inform.Add("�α� ��� ���⿡ �����Ͽ� ���� ���� ������ 10% ���ݷ��� ���ظ� �ְ� ź�� ������ ���� 14.5 ~ 33.5% ���ݷ��� ���ظ� �Խ��ϴ�. 0.05�ʸ��� �߻��ϸ� 2.5�ʰ� ���ӵ˴ϴ�.\n\n��Ÿ�� : 3��");
		inform.Add("�α� ��� ���⿡ �����Ͽ� ���� ���� ������ 15% ���ݷ��� ���ظ� �ְ� ź�� ������ ���� 15 ~ 34% ���ݷ��� ���ظ� �Խ��ϴ�. 0.05�ʸ��� �߻��ϸ� 2.5�ʰ� ���ӵ˴ϴ�.\n\n��Ÿ�� : 3��");
		inform.Add("�α� ��� ���⿡ �����Ͽ� ���� ���� ������ 20% ���ݷ��� ���ظ� �ְ� ź�� ������ ���� 15.5 ~ 34.5% ���ݷ��� ���ظ� �Խ��ϴ�. 0.05�ʸ��� �߻��ϸ� 2.5�ʰ� ���ӵ˴ϴ�.\n\n��Ÿ�� : 3��");
		inform.Add("�α� ��� ���⿡ �����Ͽ� ���� ���� ������ 25% ���ݷ��� ���ظ� �ְ� ź�� ������ ���� 16 ~ 35% ���ݷ��� ���ظ� �Խ��ϴ�. 0.05�ʸ��� �߻��ϸ� 2.5�ʰ� ���ӵ˴ϴ�.\n\n��Ÿ�� : 3��");
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
