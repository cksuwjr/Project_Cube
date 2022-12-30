using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Attack
{
    //public CubeController controller;

    // Move
    public EnemyMovement move;
    // Attack
    //string attack = "";
    public bool isEnemyNear = false;
    public bool isAttackAble = true;

    void Start()
    {
        
    }
    void Update()
    {
        if (isEnemyNear && isAttackAble)
            attack = "BasicAttack";
    }
    IEnumerator AttackDelay()
    {
        isAttackAble = false;
        yield return new WaitForSeconds(1f);
        isAttackAble = true;
    }
    private void FixedUpdate()
    {
        if (attack == null || attack == "") return;

        if (attack == "BasicAttack")
        {
            StartCoroutine(AttackDelay());
            controller.Attack(new Vector3(1, 1, 1));
        }

        attack = "";
    }
    /*
    public float CalcDamage(GameObject attacker, GameObject deffender)
    {
        return attacker.GetComponent<Status>().AttackPower - deffender.GetComponent<Status>().DeffensePower;
    }
    public bool isAttackTarget(GameObject deffender)
    {
        if (deffender == gameObject)
            return false;

        return true;
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == move.Target) isEnemyNear = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == move.Target) isEnemyNear = false;
    }
}
