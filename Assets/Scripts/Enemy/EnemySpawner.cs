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
        int count = 0;
        while(count < howmany)
        {
            GameObject spawned = Instantiate(Monster, transform.position, Quaternion.identity);
            spawned.SetActive(true);
            count++;
            yield return new WaitForSeconds(SpawnTerm);
        }
        if (Monster == Enemy)
            StartCoroutine(NextStage(10 + 5 * Stage));
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
            SpawnTerm = 2 - (Stage * 0.17f);

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
