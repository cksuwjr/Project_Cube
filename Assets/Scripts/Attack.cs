using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public CubeController controller;

    [SerializeField] private Transform AttackPos;
    [SerializeField] private LayerMask EnemyLayer;

    string attack = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && gameObject.name == "Cube") 
            attack = "BasicAttack";
    }
    private void FixedUpdate()
    {
        if (attack == null || attack == "") return;
        
        controller.Attack(attack);
        attack = "";
    }

    public void BasicAttack()
    {
        Collider[] colliders = Physics.OverlapBox(AttackPos.position, new Vector3(1, 1, 1), Quaternion.identity, EnemyLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            GameObject Deffender = colliders[i].gameObject;
            if (isAttackTarget(Deffender))
                if (Deffender.GetComponent<Hit>())
                {
                    Hit hit = Deffender.GetComponent<Hit>();
                    if (hit.isHitTarget())
                        hit.OnHit(gameObject, CalcDamage(gameObject, Deffender), false);
                }
                else 
                    Debug.Log("공격대상이 Hit 컴포넌트가 없습니다.");
        }
    }
    private float CalcDamage(GameObject attacker, GameObject deffender)
    {
        return attacker.GetComponent<Status>().AttackPower - deffender.GetComponent<Status>().DeffensePower;
    }
    private bool isAttackTarget(GameObject deffender)
    {
        if (deffender == gameObject) 
            return false;
        
        return true;
    }
}
