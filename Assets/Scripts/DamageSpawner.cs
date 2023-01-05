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
            Spawned = Instantiate(DamageSkin, transform.position, Quaternion.identity);
        else
            Spawned = Instantiate(HealSkin, transform.position, Quaternion.identity);
        Spawned.GetComponentInChildren<Text>().text = index.ToString();
        StartCoroutine(Floating(Spawned));
        Destroy(Spawned, 1.2f);

    }
    IEnumerator Floating(GameObject skin)
    {
        skin.transform.rotation = Quaternion.Euler(90, 0, 0);
        while (skin)
        {
            skin.transform.position += new Vector3(0, 0, 0.01f);
            //skin.GetComponent<RectTransform>().position += new Vector3(0, 0.1f, 0);
            yield return null;
        }
    }
}
