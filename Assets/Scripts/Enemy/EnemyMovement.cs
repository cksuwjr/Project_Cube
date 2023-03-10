using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public CubeController controller;
    public float Speed = 40f;
    float MoveDir = 0f;
    float MoveIndex = 0f;

    bool Jump = false;

    public GameObject Target;

    // Attack
    public EnemyAttack attack;
    public bool isEnemybeInStraightLine;

    // UnderCheck
    [SerializeField] private Transform Undercheck;
    [SerializeField] private LayerMask WhatisGround;

    // Start is called before the first frame update
    void Start()
    {
        if (Target == null)
            if (GameObject.Find("Cube"))
                Target = GameObject.Find("Cube").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        MoveIndex = Speed;
        
        if (Target == null) return;
        // 각도

        Transform TargetTransform = Target.GetComponent<Transform>();
        Transform MyTransform = GetComponent<Transform>();
        float horizontalDir = 0;
        float verticalDir = 0;

        isEnemybeInStraightLine = false;

        // Input horizon
        float stopRange = 0.25f;

        if ((TargetTransform.position.x < MyTransform.position.x + stopRange && TargetTransform.position.x > MyTransform.position.x - stopRange))
        {
            horizontalDir = 0;
            isEnemybeInStraightLine = true;
        }
        else
        {
            horizontalDir = TargetTransform.position.x < MyTransform.position.x ? -1 : 1;
            // 해당방향에 지면이 있나 체크 이후 없으면 해당방향 움직이지 않음 (가로)
            Collider[] colliders = Physics.OverlapBox(transform.position + Vector3.down + new Vector3(horizontalDir, 0, 0), new Vector3(0.5f, 1, 0.5f), Quaternion.identity, WhatisGround);
            if (colliders.Length == 0)
                horizontalDir = 0;
        }
        // Input vertical
        if ((TargetTransform.position.z < MyTransform.position.z + stopRange && TargetTransform.position.z > MyTransform.position.z - stopRange))
        {
            verticalDir = 0;
            isEnemybeInStraightLine = true;
        }
        else
        {
            verticalDir = TargetTransform.position.z < MyTransform.position.z ? -1 : 1;
            // 해당방향에 지면이 있나 체크 이후 없으면 해당방향 움직이지 않음 (세로)
            Collider[] colliders = Physics.OverlapBox(transform.position + Vector3.down + new Vector3(0, 0, verticalDir), new Vector3(0.5f, 1, 0.5f), Quaternion.identity, WhatisGround);
            if (colliders.Length == 0)
                verticalDir = 0;
        }

        //if ((horizontalDir * verticalDir) == 1)
        //    isEnemybeInStraightLine = true;
       

        // Angle by input
        int HorizonAngle = 0;
        if (horizontalDir == 1)
            HorizonAngle = 90;
        if (horizontalDir == -1)
            HorizonAngle = 270;

        int VerticalAngle = 0;
        if (verticalDir == 1)
            if (horizontalDir == 1)
                VerticalAngle = 0;
            else
                VerticalAngle = 360;
        if (verticalDir == -1)
            VerticalAngle = 180;

        if (HorizonAngle + VerticalAngle == 0)
            MoveIndex = 0;
        else
            if (horizontalDir == 0 || verticalDir == 0)
            MoveDir = ((HorizonAngle + VerticalAngle) % 360);
        else
            MoveDir = (HorizonAngle + VerticalAngle) / 2;

        // Jump 유무


        if (Target == null) { MoveIndex = 0; Jump = true; }

        
    }
    
    private void FixedUpdate()
    {
        
        // Move
        if (attack.isAttackAble && !controller.IsBinded && controller.IsActable)
            controller.Move(MoveDir, MoveIndex * Time.fixedDeltaTime, Jump);
        Jump = false;



        //////////////////////
    }
}
