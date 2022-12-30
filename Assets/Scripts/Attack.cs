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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && gameObject.name == "Cube") 
            attack = "BasicAttack";
    }
    private void FixedUpdate()
    {
        if (attack == null || attack == "") return;
        
        if (attack == "BasicAttack")
            controller.Attack(new Vector3(1, 1, 1));


        attack = "";
    }
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
}
