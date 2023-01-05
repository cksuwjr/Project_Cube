using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    float count = 0;
    float SpawnTerm = 2;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", 3);
    }

    // Update is called once per frame
    void Spawn()
    {
        GameObject spawned = Instantiate(Enemy, transform.position, Quaternion.identity);
        spawned.SetActive(true);
        spawned.GetComponent<EnemyMovement>().Speed = 15f + count++;
        //Debug.Log("¼ÒÈ¯" + ++count);
        if (SpawnTerm > 0.3)
            Invoke("Spawn", SpawnTerm -= count * 0.01f);
        else
            Invoke("Spawn", 0.3f);
        if (!GameObject.Find("Cube"))
            CancelInvoke("Spawn");
        
    }
}
