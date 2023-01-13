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
        SkillStart(myColor + new Color(0.3f, -0.15f, -0.15f));
        yield return new WaitForSeconds(delay);
        controller.Attack(new Vector3(1, 1, 1));
        SkillEnd();
    }
    IEnumerator Shooting(float delay)
    {
        SkillStart(myColor + new Color(0.3f, -0.15f, -0.15f));
        yield return new WaitForSeconds(delay);
        controller.Skill(controller.Skill_Q);
        SkillEnd();
    }
    IEnumerator Q(float delay)
    {
        controller.Skill(controller.Skill_Q);
        yield return new WaitForSeconds(delay);
        SkillEnd();
    }
    IEnumerator W(float delay)
    {
        if (controller.isGround)
           GetComponent<Rigidbody>().AddForce(new Vector3(0, 500f, 0));
        yield return new WaitForSeconds(delay);
        controller.Skill(controller.Skill_W);
        SkillEnd();
    }
    IEnumerator E(float delay)
    {
        controller.Skill(controller.Skill_E);
        yield return new WaitForSeconds(delay);
        SkillEnd();
    }
    IEnumerator R(float delay)
    {
        controller.Skill(controller.Skill_R);
        yield return new WaitForSeconds(delay);
        SkillEnd();
    }
    private void SkillStart(Color color)
    {
        isAttackAble = false;
        render.material.color = color;
        GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
    }
    private void SkillEnd()
    {
        isAttackAble = true;
        render.material.color = myColor;
        GetComponent<Rigidbody>().velocity = new Vector3(0,GetComponent<Rigidbody>().velocity.y, 0);
    }
    private void Update() { }

    private void FixedUpdate()
    {
        if (isShootableEnemy)
            if (move.isEnemybeInStraightLine && isAttackAble && !controller.IsBinded && controller.IsActable)
                attack = "ShootingAttack";

        if (isEnemyNear && isAttackAble && !controller.IsBinded && controller.IsActable)
            attack = "BasicAttack";

        int rand = Random.Range(0, 1000);
        if (0 < rand && rand < 5)
            attack = "Random";


        if (attack == null || attack == "") return;

        if (attack == "BasicAttack" && isAttackAble)
        {
            StartCoroutine(Attack(0.45f));
        }
        
        if (isShootableEnemy)
            if (attack == "ShootingAttack" && isAttackAble)
                StartCoroutine(Shooting(0.65f));

        
        //if (!move.isEnemybeInStraightLine && !isEnemyNear)

        if (attack == "Random" && isShootableEnemy && isAttackAble)
        {
            
            if (rand == 1 && controller.Skill_Q) {
                SkillStart(Color.blue);
                StartCoroutine(Q(0.1f));
            }
            else if (rand == 2 && controller.Skill_W)
            {
                SkillStart(Color.black);
                StartCoroutine(W(1.35f));
            }
            else if (rand == 3 && controller.Skill_E) {
                SkillStart(Color.blue);
                StartCoroutine(E(0.1f));
            }
            else if (rand == 4 && controller.Skill_R)
            {
                SkillStart(Color.blue);
                StartCoroutine(R(2.5f));
            }
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
