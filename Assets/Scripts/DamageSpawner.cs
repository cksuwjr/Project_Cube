using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageSpawner : MonoBehaviour
{
    public GameObject DamageSkin;
    public GameObject HealSkin;
    
    public void Spawn(float index)
    {
        GameObject Spawned;
        if (index < 0)
            Spawned = Instantiate(DamageSkin, transform.position + new Vector3(0,1,0), Quaternion.identity);
        else
            Spawned = Instantiate(HealSkin, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        Spawned.GetComponentInChildren<Text>().text = ((int)index).ToString();
    }
    
}
