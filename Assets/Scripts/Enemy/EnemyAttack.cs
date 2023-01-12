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
    IEnumerator Attack(float delay)
    {
        isAttackAble = false;
        
        render.material.color = myColor + new Color(0.3f,-0.15f, -0.15f);
        yield return new WaitForSeconds(delay);
        controller.Attack(new Vector3(1, 1, 1));
        render.material.color = myColor;

        isAttackAble = true;
    }
    IEnumerator Shooting(float delay)
    {
        isAttackAble = false;

        render.material.color = myColor + new Color(0.3f, -0.15f, -0.15f);
        yield return new WaitForSeconds(delay);
        controller.Skill(controller.Skill_Q);
        render.material.color = myColor;

        isAttackAble = true;
    }
    IEnumerator Q(float delay)
    {
        isAttackAble = false;

        controller.Skill(controller.Skill_Q);
        yield return new WaitForSeconds(delay);

        isAttackAble = true;
    }
    IEnumerator W(float delay)
    {
        isAttackAble = false;

        if (controller.isGround)
           GetComponent<Rigidbody>().AddForce(new Vector3(0, 100f, 0));
        yield return new WaitForSeconds(delay);
        controller.Skill(controller.Skill_W);

        isAttackAble = true;
    }
    IEnumerator E(float delay)
    {
        isAttackAble = false;

        controller.Skill(controller.Skill_E);
        yield return new WaitForSeconds(delay);

        isAttackAble = true;
    }
    IEnumerator R(float delay)
    {
        isAttackAble = false;

        controller.Skill(controller.Skill_R);
        yield return new WaitForSeconds(delay);

        isAttackAble = true;
    }
    private void Update() { }

    private void FixedUpdate()
    {
        if (isShootableEnemy)
            if (move.isEnemybeInStraightLine && isAttackAble && !controller.IsBinded && controller.IsActable)
                attack = "ShootingAttack";

        if (isEnemyNear && isAttackAble && !controller.IsBinded && controller.IsActable)
            attack = "BasicAttack";

        if (!move.isEnemybeInStraightLine && !isEnemyNear)
            attack = "Random";


        if (attack == null || attack == "") return;

        if (attack == "BasicAttack" && isAttackAble)
        {
            StartCoroutine(Attack(0.45f));
        }
        
        if (isShootableEnemy)
            if (attack == "ShootingAttack" && isAttackAble)
                StartCoroutine(Shooting(0.65f));

        
        if (attack == "Random" && isShootableEnemy && isAttackAble)
        {
            int rand = Random.Range(0, 5);
            if (rand == 1) { }
            else if (rand == 2)
                StartCoroutine(W(0.3f));
            else if (rand == 3) { }
            //StartCoroutine(E(0.5f));
            else if (rand == 4)
                StartCoroutine(R(2.5f));
        }


        attack = "";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == move.Target) isEnemyNear = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == move.Target) isEnemyNear = false;
    }
}
