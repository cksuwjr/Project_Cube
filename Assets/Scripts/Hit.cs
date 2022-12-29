using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public bool isHitTarget()
    {
        return true;
    }
    public void OnHit(GameObject attacker, float damage, bool isCritical)
    {
        GetComponent<Status>().Hp -= damage;
        if (GetComponent<Status>().Hp < 0)
            Destroy(gameObject);
        Debug.Log(gameObject.name + "가 " + attacker.name + "로부터 " + damage + "의 피해를 입었습니다! 남은체력: " + GetComponent<Status>().Hp);
    }
}
