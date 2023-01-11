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
    Renderer render;
    Color myColor;

    public bool isShootableEnemy = false;

    private void Start()
    {
        render = GetComponent<Renderer>();
        myColor = render.material.color;
    }
    IEnumerator AttackDelay(float time)
    {
        isAttackAble = false;
        yield return new WaitForSeconds(time);
        isAttackAble = true;
        render.material.color = myColor;
    }
    IEnumerator AttackStartDelay(float time)
    {
        isAttackAble = false;
        
        render.material.color = myColor + new Color(0.3f,-0.15f, -0.15f);
        yield return new WaitForSeconds(time);
        StartCoroutine(AttackDelay(1));
        controller.Attack(new Vector3(1, 1, 1));
    }
    IEnumerator StartShooting(float time, float delay)
    {
        isAttackAble = false;
        yield return new WaitForSeconds(time);
        StartCoroutine(AttackDelay(delay));
        controller.Skill("Cast_Q");
    }
    private void FixedUpdate()
    {
        if (isShootableEnemy)
            if (move.isEnemybeInStraightLine && isAttackAble && !controller.IsBinded && controller.IsActable)
                attack = "ShootingAttack";

        if (isEnemyNear && isAttackAble && !controller.IsBinded && controller.IsActable)
            attack = "BasicAttack";


        if (attack == null || attack == "") return;

        if (attack == "BasicAttack" && isAttackAble)
        {
            StartCoroutine(AttackStartDelay(0.45f));
        }
        
        if (isShootableEnemy)
            if (attack == "ShootingAttack" && isAttackAble)
                StartCoroutine(StartShooting(0.3f, 0.075f));

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
