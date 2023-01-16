using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    
    
    [SerializeField] protected float _MaxHp = 500f;
    [SerializeField] protected float _Hp = 500f;

    [SerializeField] protected float _AttackPower = 100f;
    [SerializeField] protected float _DeffensePower = 20f;

    [SerializeField] protected float _MaxExp = 100f;
    [SerializeField] protected float _Exp = 100f;
    public float MaxHp { get { return _MaxHp; } set { _MaxHp = value; } }
    public float Hp { get { return _Hp; } set { _Hp = value; } }

    public float AttackPower { get { return _AttackPower; } set { _AttackPower = value; } }
    public float DeffensePower { get { return _DeffensePower; } set { _DeffensePower = value; } }

    public float MaxExp { get { return _MaxExp; } set { _MaxExp = value; } }
    public float Exp { get { return _Exp; } set { _Exp = value; } }
}
