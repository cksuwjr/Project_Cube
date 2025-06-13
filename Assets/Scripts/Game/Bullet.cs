using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject Fromwho;
    Vector3 direction = Vector3.zero;
    float Speed = 0;
    float Damage = 0;
    float CriticalProbablity = 0;
    float CriticalDamageMagnification = 0;

    bool DontTwiceAttack = false;
    public void Tang(GameObject fromwho, float damage, float speed = 15f, float DestroyTime = 5f, float criticalPro = 0, float magnification = 0)//Vector3 dir,
    {
        Fromwho = fromwho;
        direction = transform.forward;
        Speed = speed;
        Damage = damage;
        CriticalProbablity = criticalPro;
        CriticalDamageMagnification = magnification;

        GetComponent<Rigidbody>().velocity = new Vector3(direction.x * Speed, 0, direction.z * Speed);
        DontTwiceAttack = true;
        Destroy(gameObject, DestroyTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            Destroy(gameObject);

        if (other.gameObject != Fromwho && DontTwiceAttack)
        {
            DontTwiceAttack = false; // 두번 공격 방지
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {

                Attack attackComponent = null;
                if (Fromwho.GetComponent<Attack>()) attackComponent = Fromwho.GetComponent<Attack>();
                else if (Fromwho.GetComponent<EnemyAttack>()) attackComponent = Fromwho.GetComponent<EnemyAttack>();

                if (attackComponent == null) Destroy(gameObject);

                GameObject Deffender = other.gameObject;

                float damage = attackComponent.CalcDamage(Damage, Deffender);

                if (attackComponent.isAttackTarget(Deffender))
                    if (Deffender.GetComponent<Hit>())
                    {
                        Hit hit = Deffender.GetComponent<Hit>();
                        if (hit.isHitTarget())
                        {
                            float probablity = Random.Range(0, 100);
                            bool isCritical = probablity < CriticalProbablity ? true : false; 
                            hit.OnHit(Fromwho, damage, isCritical, CriticalDamageMagnification);
                            other.GetComponent<CubeController>().KnockBack(Fromwho, 5f);
                        }
                    }
                    else
                        Debug.Log("공격대상이 Hit 컴포넌트를 소유하지 않았습니다.");

                Destroy(gameObject);
            }
        }
    }
}
