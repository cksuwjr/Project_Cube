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
        Debug.Log(gameObject.name + "�� " + attacker.name + "�κ��� " + damage + "�� ���ظ� �Ծ����ϴ�! ����ü��: " + GetComponent<Status>().Hp);
    }
}
