using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CubeController controller;
    public float Speed = 40f;
    float MoveDir = 0f;
    float MoveIndex = 0f;

    bool Jump = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveIndex = Speed;
        // 각도
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        int HorizonAngle = 0;
        if (horizontalInput == 1)
            HorizonAngle = 90;
        if (horizontalInput == -1)
            HorizonAngle = 270;

        int VerticalAngle = 0;
        if (verticalInput == 1)
            if (horizontalInput == 1)
                VerticalAngle = 0;
            else
                VerticalAngle = 360;
        if (verticalInput == -1)
            VerticalAngle = 180;

        if (HorizonAngle + VerticalAngle == 0)
            MoveIndex = 0;
        else
            if (horizontalInput == 0 || verticalInput == 0)
                MoveDir = ((HorizonAngle + VerticalAngle) % 360);
            else
                MoveDir = (HorizonAngle + VerticalAngle) / 2;
        

        // 점프 유무
        if (Input.GetButtonDown("Jump"))
        {
            Jump = true;
        }
    }
    private void FixedUpdate()
    {
        if (!controller.IsBinded && controller.IsActable)
            controller.Move(MoveDir, MoveIndex * Time.fixedDeltaTime, Jump);
        Jump = false;
    }
}
