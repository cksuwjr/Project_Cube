using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FrozenLight : Skill
{
    public Material mat;
    private void Awake()
    {
        inform.Add("얼어붙은 빛의 반지: 방출 ");
        inform.Add("인근 모든 적은 얼어붙습니다.");
    }
    protected override IEnumerator Cast_()
    {
        Collider[] colliders = Physics.OverlapBox(controller.AttackPos.position, new Vector3(50,1,50) / 2, transform.rotation, controller.WhatIsEnemy);
        List<Collider> Enemys = new List<Collider>();
        
        for(int j = 0; j < colliders.Length; j++)
        {
            if (colliders[j].isTrigger) continue; // isTrigger가 true인 collider는 감지 제외(collider가 여럿인 개체의 중복적용 방지위한 임시방책)
            Enemys.Add(colliders[j]);
        }


        LineRenderer lr;
        if (GetComponent<LineRenderer>())
        {
            Destroy(GetComponent<LineRenderer>());
            lr = gameObject.AddComponent<LineRenderer>();
        }
        else
            lr = gameObject.AddComponent<LineRenderer>();

        // 최대 피격 수
        int MaxHitCount = 88;
        for (int j = 0; j < lr.positionCount; j++)
            lr.SetPosition(j, transform.position);
        if (lr.positionCount > Enemys.Count)
            lr.positionCount = Enemys.Count + 1;

        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.material = mat;
        lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lr.receiveShadows = false;
        lr.positionCount = 1;
        lr.SetPosition(0, controller.AttackPos.position + new Vector3(0, 1, 0));

        int i = 0;
        while (i < Enemys.Count && (i + 1) < MaxHitCount )
        {
            if (Enemys[i])
            {
                lr.positionCount += 1;
                try
                {
                    lr.SetPosition(i + 1, Enemys[i].transform.position + new Vector3(0, 1, 0));
                }
                catch { lr.SetPosition(i + 1, transform.position); }
                Enemys[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                Enemys[i].GetComponent<CubeController>().GetDamage(9999, controller.gameObject);
                Enemys[i].GetComponent<CubeController>().StartCoroutine(Enemys[i].GetComponent<CubeController>().CC(0.5f));

                if (lr.startWidth < 0.25f)
                {
                    lr.startWidth += 0.05f;
                    lr.endWidth += 0.05f;
                }
            }
            i += 1;
            Debug.Log("완료" + i);
            yield return new WaitForSeconds(0.03f);
            
        }
        yield return new WaitForSeconds(0.13f);
        Destroy(lr);

        /*
        for (int i = 0; i < colliders.Length; i++) {
            lr.positionCount += 1;
            lr.SetPosition(i + 1, colliders[i].transform.position + new Vector3(0,1,0));
            colliders[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            colliders[i].GetComponent<CubeController>().GetDamage(99);
            colliders[i].GetComponent<CubeController>().StartCoroutine(colliders[i].GetComponent<CubeController>().CC(1.5f));
        }
        float time = 0;
        while(time < 0.3f)
        {
            lr.startWidth += 0.05f;
            lr.endWidth += 0.05f;
            time += 0.1f;
            yield return new WaitForSeconds(0.1f);

        }
        */
    }
}
