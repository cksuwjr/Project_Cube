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
    public void OnHit(GameObject attacker, float damage, bool isCritical, float magnification = 2)
    {
        if (isCritical)
            GetComponent<CubeController>().GetDamage(damage * magnification, attacker, true);
        else
            GetComponent<CubeController>().GetDamage(damage, attacker);
    }
}
