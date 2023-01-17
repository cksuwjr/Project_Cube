using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Shooting : Skill
{
	[SerializeField] private GameObject Bullet;
    private void Awake()
    {
        inform.Add("[Q] Shooting ");

        inform.Add("�������� ź���� �߻��Ͽ� ������ ������ 20 + 32% ���ݷ� �� ���ظ� �����ϴ�.\n\n��Ÿ�� : 0.01��");
        inform.Add("�������� ź���� �߻��Ͽ� ������ ������ 35 + 49% ���ݷ� �� ���ظ� �����ϴ�.\n\n��Ÿ�� : 0.01��");
        inform.Add("�������� ź���� �߻��Ͽ� ������ ������ 50 + 66% ���ݷ� �� ���ظ� �����ϴ�.\n\n��Ÿ�� : 0.01��");
        inform.Add("�������� ź���� �߻��Ͽ� ������ ������ 65 + 83% ���ݷ� �� ���ظ� �����ϴ�.\n\n��Ÿ�� : 0.01��");
        inform.Add("�������� ź���� �߻��Ͽ� ������ ������ 80 + 100% ���ݷ� �� ���ظ� �����ϴ�.\n\n��Ÿ�� : 0.01��");
    }
    protected override IEnumerator Cast_()
    {
        float basic_Attack_damage = 5 + (15 * skill_Level);
        float Attack_coefficient = 0.15f + (0.17f * skill_Level);
        Instantiate(Bullet, transform.position, transform.rotation).GetComponent<Bullet>().Tang(gameObject, basic_Attack_damage + (GetComponent<Status>().AttackPower * Attack_coefficient), 30, 0.8f);

        yield return null;
    }
}
