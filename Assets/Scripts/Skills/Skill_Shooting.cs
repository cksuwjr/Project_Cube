using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Shooting : Skill
{
	[SerializeField] private GameObject Bullet;
    private void Awake()
    {
        inform.Add("[Q] Shooting ");

        inform.Add("�������� ź���� �߻��Ͽ� ������ ������ 40% ���ݷ� �� ���ظ� �����ϴ�. 0% Ȯ���� ġ��Ÿ�� ���� 1���� ������ 40% ���ݷ��� ���ظ� �����ϴ�.\n\n��Ÿ�� : 0.01��");
        inform.Add("�������� ź���� �߻��Ͽ� ������ ������ 60% ���ݷ� �� ���ظ� �����ϴ�. 5% Ȯ���� ġ��Ÿ�� ���� 1.3���� ���ظ� 78% ���ݷ��� ���ظ� �����ϴ�.\n\n��Ÿ�� : 0.01��");
        inform.Add("�������� ź���� �߻��Ͽ� ������ ������ 80% ���ݷ� �� ���ظ� �����ϴ�. 10% Ȯ���� ġ��Ÿ�� ���� 1.6���� ���ظ� 128% ���ݷ��� ���ظ� �����ϴ�.\n\n��Ÿ�� : 0.01��");
        inform.Add("�������� ź���� �߻��Ͽ� ������ ������ 100% ���ݷ� �� ���ظ� �����ϴ�. 15% Ȯ���� ġ��Ÿ�� ���� 1.9���� ���ظ� 190% ���ݷ��� ���ظ� �����ϴ�.\n\n��Ÿ�� : 0.01��");
        inform.Add("�������� ź���� �߻��Ͽ� ������ ������ 120% ���ݷ� �� ���ظ� �����ϴ�. 20% Ȯ���� ġ��Ÿ�� ���� 2.2���� ���ظ� 252% ���ݷ��� ���ظ� �����ϴ�.\n\n��Ÿ�� : 0.01��");
        inform.Add("�������� ź���� �߻��Ͽ� ������ ������ 140% ���ݷ� �� ���ظ� �����ϴ�. 25% Ȯ���� ġ��Ÿ�� ���� 2.5���� ���ظ� 350% ���ݷ��� ���ظ� �����ϴ�.\n\n��Ÿ�� : 0.01��");
    }
    protected override IEnumerator Cast_()
    {
        float basic_Attack_damage = 5 + (15 * skill_Level);
        float Attack_coefficient = 0.15f + (0.2f * skill_Level);
        float CriticalProbablity = -5 + 5 * skill_Level;
        float CriticalDamageMagnification = 0.7f + 0.3f * skill_Level;
        float ShootSpeed = 20 + 5 * skill_Level;
        

        Instantiate(Bullet, transform.position, transform.rotation).GetComponent<Bullet>().Tang(gameObject, basic_Attack_damage + (GetComponent<Status>().AttackPower * Attack_coefficient), ShootSpeed, 0.8f, CriticalProbablity, CriticalDamageMagnification);

        yield return null;
    }
}
