using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Boss;

    public CubeUI cubeUI;
    private enum Mode {
        StageMode,
        InfinityMode,
    }
    [SerializeField]private Mode mode = Mode.StageMode;

    [Header("Stage Mode")]
    public int Stage = 0;
    List<List<int>> SpawnMobCount = new List<List<int>>() 
    { 
        // normalmob, hardmob
        new List<int>{ 5, 0},
        new List<int>{ 15, 0},
        new List<int>{ 35, 1},
        new List<int>{ 75, 0},
        new List<int>{ 125, 0},
        new List<int>{ 195, 3},
        new List<int>{ 275, 0},
        new List<int>{ 365, 0},
        new List<int>{ 475, 7},
        new List<int>{ 1000, 100},
    };
    


    [Header("Infinity Mode")]
    public float SpawnTerm = 2;

    public float FasterSpeed = 0f;
    public float FasterSpawn = 1f; 

    //[Header("Both use Var")]
    float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", 3);
    }

    // Update is called once per frame
    void Spawn()
    {
        
        if (mode == Mode.StageMode)
        {
            if (count < SpawnMobCount[Stage][1]) // 보스소환
            {
                GameObject spawned = Instantiate(Boss, transform.position, Quaternion.identity);
                spawned.SetActive(true);
            }
            else
            {
                if (Random.Range(Stage, 100) > 95 && Stage > 2)
                {
                    GameObject spawned = Instantiate(Boss, transform.position, Quaternion.identity);
                    spawned.SetActive(true);
                    Debug.Log("급작스런 출현!");
                }

            }

            if (count++ < SpawnMobCount[Stage][0]) // 잡몹소환
            {
                GameObject spawned = Instantiate(Enemy, transform.position, Quaternion.identity);
                spawned.SetActive(true);
                if (SpawnTerm > 0.3)
                    Invoke("Spawn", SpawnTerm -= count * 0.01f * FasterSpawn);
                else
                    Invoke("Spawn", 0.3f);
            }
            else
            {
                Stage++;
                count = 0;
                Debug.Log("현재 스테이지 " + (Stage + 1) + ", 쉬는시간 0초");
                if (cubeUI)
                    cubeUI.PopupStageUI("Stage " + (Stage + 1));
                Invoke("Spawn", 0);
            }
        }
        else if (mode == Mode.InfinityMode)
        {
            GameObject spawned = Instantiate(Enemy, transform.position, Quaternion.identity);
            spawned.SetActive(true);

            spawned.GetComponent<EnemyMovement>().Speed = 15f + (count++) * 0.01f * FasterSpeed;
            //Debug.Log("소환" + ++count);
            if (SpawnTerm > 0.3)
                Invoke("Spawn", SpawnTerm -= count * 0.01f * FasterSpawn);
            else
                Invoke("Spawn", 0.3f);
            if (!GameObject.Find("Cube"))
                CancelInvoke("Spawn");
        }
    }
}
