using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public CubeController controller;

    public string attack = "";
    
    void Start()
    {
               
    }

    // Update is called once per frame
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A)) 
            //attack = "BasicAttack";
        if (Input.GetKeyDown(KeyCode.Q))
            attack = "Q";
        if (Input.GetKeyDown(KeyCode.W))
            attack = "W";
        if (Input.GetKeyDown(KeyCode.E))
            attack = "E";
        if (Input.GetKeyDown(KeyCode.R))
            attack = "R";
    }
    private void FixedUpdate()
    {
        if (attack == null || attack == "") return;

        if (attack == "BasicAttack")
            controller.Attack(new Vector3(1, 1, 1));
        else if (attack == "Q")
            controller.Q();
        else if (attack == "W")
            controller.W();
        else if (attack == "E")
            controller.E();
        else if (attack == "R")
            controller.R();
        attack = "";
    }
    public float CalcDamage(GameObject attacker, GameObject deffender)
    {
        
        return attacker.GetComponent<Status>().AttackPower - (attacker.GetComponent<Status>().AttackPower * (deffender.GetComponent<Status>().DeffensePower / (100 + deffender.GetComponent<Status>().DeffensePower)));


    }
    public bool isAttackTarget(GameObject deffender)
    {
        if (deffender == gameObject) 
            return false;
        
        return true;
    }
}
