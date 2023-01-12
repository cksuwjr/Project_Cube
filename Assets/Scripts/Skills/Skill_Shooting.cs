using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Shooting : Skill
{
	[SerializeField] private GameObject Bullet;
    protected override IEnumerator Cast_()
    {
        Instantiate(Bullet, transform.position, transform.rotation).GetComponent<Bullet>().Tang(gameObject, GetComponent<Status>().AttackPower * 0.5f, 30, 0.8f);
        yield return null;
    }
}
