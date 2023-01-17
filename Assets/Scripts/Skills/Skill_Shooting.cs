using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Shooting : Skill
{
	[SerializeField] private GameObject Bullet;
    private void Awake()
    {
        inform.Add("[Q] Shooting ");

        inform.Add("전방으로 탄알을 발사하여 적중한 적에게 20 + 32% 공격력 의 피해를 입힙니다.\n\n쿨타임 : 0.01초");
        inform.Add("전방으로 탄알을 발사하여 적중한 적에게 35 + 49% 공격력 의 피해를 입힙니다.\n\n쿨타임 : 0.01초");
        inform.Add("전방으로 탄알을 발사하여 적중한 적에게 50 + 66% 공격력 의 피해를 입힙니다.\n\n쿨타임 : 0.01초");
        inform.Add("전방으로 탄알을 발사하여 적중한 적에게 65 + 83% 공격력 의 피해를 입힙니다.\n\n쿨타임 : 0.01초");
        inform.Add("전방으로 탄알을 발사하여 적중한 적에게 80 + 100% 공격력 의 피해를 입힙니다.\n\n쿨타임 : 0.01초");
    }
    protected override IEnumerator Cast_()
    {
        float basic_Attack_damage = 5 + (15 * skill_Level);
        float Attack_coefficient = 0.15f + (0.17f * skill_Level);
        Instantiate(Bullet, transform.position, transform.rotation).GetComponent<Bullet>().Tang(gameObject, basic_Attack_damage + (GetComponent<Status>().AttackPower * Attack_coefficient), 30, 0.8f);

        yield return null;
    }
}
