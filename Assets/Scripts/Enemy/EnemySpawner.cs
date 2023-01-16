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

    [SerializeField] private List<Vector2> SpawnMobCount;
    

    [Header("Infinity Mode")]
    public float SpawnTerm = 2;

    public float FasterSpeed = 0f;
    public float FasterSpawn = 1f;

    bool isAllSpawned_Enemy = false;
    bool isAllSpawned_Boss = false;


    static GameObject Spawned;

    void Start()
    {
        if (mode == Mode.StageMode)
        {
            StartCoroutine(Spawn(Enemy, (int)SpawnMobCount[Stage].x));
            StartCoroutine(Spawn(Boss, (int)SpawnMobCount[Stage].y));
        }
    }
    IEnumerator Spawn(GameObject Monster, int howmany)
    {
        if (Monster == Enemy)
            isAllSpawned_Enemy = false;
        if (Monster == Boss)
            isAllSpawned_Boss = false;
        int count = 0;
        while(count < howmany)
        {
            GameObject Spawned = Instantiate(Monster, transform.position, Quaternion.identity);
            Spawned.SetActive(true);
            count++;
            yield return new WaitForSeconds(SpawnTerm);
        }
        if (Monster == Enemy)
            isAllSpawned_Enemy = true;
        if (Monster == Boss)
            isAllSpawned_Boss = true;

        if (isAllSpawned_Enemy && isAllSpawned_Boss && cubeUI) { 
            StartCoroutine(NextStage(5 + Stage));
        }
    }
    IEnumerator NextStage(float time)
    {
        yield return new WaitForSeconds(time);

        if (Stage < SpawnMobCount.Count)
        {

            Stage++;
            Debug.Log("현재 스테이지 " + (Stage + 1));
            if (cubeUI)
                cubeUI.PopupStageUI("Stage " + (Stage + 1));
            SpawnTerm = 3 - (Stage * 0.27f);

            StartCoroutine(Spawn(Enemy, (int)SpawnMobCount[Stage].x));
            StartCoroutine(Spawn(Boss, (int)SpawnMobCount[Stage].y));
        }
        else
        {
            if (cubeUI)
                cubeUI.PopupStageUI("Stage All Clear!!!");
        }

    }
}
