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

    // Start is called before the first frame update
    void Start()
    {
        if (Target == null)
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


        // Input
        
        if (TargetTransform.position.x < MyTransform.position.x + 0.15 && TargetTransform.position.x > MyTransform.position.x - 0.15) { horizontalDir = 0; } else { horizontalDir = TargetTransform.position.x < MyTransform.position.x ? -1 : 1; }
        if (TargetTransform.position.z < MyTransform.position.z + 0.15 && TargetTransform.position.z > MyTransform.position.z - 0.15) { verticalDir = 0; } else { verticalDir = TargetTransform.position.z < MyTransform.position.z ? -1 : 1; }
        

        // Angle
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

    }
    private void FixedUpdate()
    {
        controller.Move(MoveDir, MoveIndex * Time.fixedDeltaTime, Jump);
        Jump = false;
    }
}
