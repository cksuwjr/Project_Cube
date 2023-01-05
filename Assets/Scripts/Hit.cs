using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public bool isHittable = true;
    public bool isHitTarget()
    {
        return isHittable;
    }
    public void OnHit(GameObject attacker, float damage, bool isCritical)
    {
        GetComponent<CubeController>().GetDamage(damage);
    }
}
