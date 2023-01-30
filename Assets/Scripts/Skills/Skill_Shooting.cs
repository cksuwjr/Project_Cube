using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Shooting : Skill
{
	[SerializeField] private GameObject Bullet;
    private void Awake()
    {
        inform.Add("[Q] Shooting ");

        inform.Add("전방으로 탄알을 발사하여 적중한 적에게 40% 공격력 의 피해를 입힙니다. 0% 확률로 치명타를 입혀 1배의 피해인 40% 공격력의 피해를 입힙니다.\n\n쿨타임 : 0.01초");
        inform.Add("전방으로 탄알을 발사하여 적중한 적에게 60% 공격력 의 피해를 입힙니다. 5% 확률로 치명타를 입혀 1.3배의 피해를 78% 공격력의 피해를 입힙니다.\n\n쿨타임 : 0.01초");
        inform.Add("전방으로 탄알을 발사하여 적중한 적에게 80% 공격력 의 피해를 입힙니다. 10% 확률로 치명타를 입혀 1.6배의 피해를 128% 공격력의 피해를 입힙니다.\n\n쿨타임 : 0.01초");
        inform.Add("전방으로 탄알을 발사하여 적중한 적에게 100% 공격력 의 피해를 입힙니다. 15% 확률로 치명타를 입혀 1.9배의 피해를 190% 공격력의 피해를 입힙니다.\n\n쿨타임 : 0.01초");
        inform.Add("전방으로 탄알을 발사하여 적중한 적에게 120% 공격력 의 피해를 입힙니다. 20% 확률로 치명타를 입혀 2.2배의 피해를 252% 공격력의 피해를 입힙니다.\n\n쿨타임 : 0.01초");
        inform.Add("전방으로 탄알을 발사하여 적중한 적에게 140% 공격력 의 피해를 입힙니다. 25% 확률로 치명타를 입혀 2.5배의 피해를 350% 공격력의 피해를 입힙니다.\n\n쿨타임 : 0.01초");
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
