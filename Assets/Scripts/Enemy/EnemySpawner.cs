using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public CubeUI cubeUI;
    private enum Mode {
        StageMode,
        InfinityMode,
    }
    [SerializeField]private Mode mode = Mode.StageMode;

    [Header("Stage Mode")]
    public int Stage = 0;

    public GameObject danger;

    [Header("Infinity Mode")]
    public float SpawnTerm = 2;

    public float FasterSpeed = 0f;
    public float FasterSpawn = 1f;

    private void Start()
    {
        Invoke("Init", 1);
        //Init();
    }

    public void Init()
    {
        if (mode == Mode.StageMode)
            NextStage();
    }


    private void SpawnEnemy(Vector3 position)
    {
        GameObject spawned = PoolManager.Instance.enemyPool.GetPoolObject();
        spawned.transform.position = position;
        spawned.GetComponent<Status>().MaxHp += ((Stage - 4) * 150);
        spawned.GetComponent<Status>().Hp = spawned.GetComponent<Status>().MaxHp;
        spawned.SetActive(true);
    }

    private void SpawnBoss(Vector3 position)
    {
        GameObject spawned = PoolManager.Instance.bossPool.GetPoolObject();
        spawned.transform.position = position;
        spawned.GetComponent<Status>().MaxHp += ((Stage - 4) * 150);
        spawned.GetComponent<Status>().Hp = spawned.GetComponent<Status>().MaxHp;
        spawned.SetActive(true);

        if ((Stage - 4) > 0)
        {
            if ((Stage - 4) < 5)
            {
                spawned.GetComponent<CubeController>().Skill_Q.skill_Level = Stage - 4;
                spawned.GetComponent<CubeController>().Skill_W.skill_Level = Stage - 4;
                spawned.GetComponent<CubeController>().Skill_E.skill_Level = Stage - 4;
                spawned.GetComponent<CubeController>().Skill_R.skill_Level = Stage - 4;
            }
            else
            {
                spawned.GetComponent<CubeController>().Skill_Q.skill_Level = 5;
                spawned.GetComponent<CubeController>().Skill_W.skill_Level = 5;
                spawned.GetComponent<CubeController>().Skill_E.skill_Level = 5;
                spawned.GetComponent<CubeController>().Skill_R.skill_Level = 5;
            }
        }
    }

    IEnumerator StageSpawn()
    {
        int count = 0;
        StageData stageData = DataManager.Instance.GetStageData(Stage - 1);


        while (count < stageData.enemyCount || count < stageData.bossCount)
        {
            Debug.Log(stageData.stage + "stage : " + stageData.enemyCount);
            if (count < stageData.enemyCount)
            {
                for(int i = 0; i < 4; i++)
                    SpawnEnemy(GetSpawnPosition());

            }
            if (count < stageData.bossCount)
            {
                for (int i = 0; i < 4; i++)
                    SpawnBoss(GetSpawnPosition());
            }

            count++;
            yield return YieldInstructionCache.WaitForSeconds(SpawnTerm);
        }

        yield return YieldInstructionCache.WaitForSeconds(10 + Stage);

        NextStage();
    }

    private void NextStage()
    {
        if (Stage < DataManager.Instance.GetStageCount())
        {
            Stage++;
            Debug.Log("현재 스테이지 " + (Stage));
            if (cubeUI)
                cubeUI.PopupStageUI("Stage " + (Stage));
            SpawnTerm = 3 - (Stage * 0.27f);
            if (SpawnTerm < 0.3f)
                SpawnTerm = 0.3f;
            if (Stage == 4 || Stage == 6 || Stage == 8 || Stage == 10)
                if (danger)
                {
                    Instantiate(danger, new Vector3(0, 1, 0), Quaternion.Euler(90,0,0)).SetActive(true);
                }

            StartCoroutine("StageSpawn");
        }
        else
        {
            if (cubeUI)
                cubeUI.PopupStageUI("Stage All Clear!!!");
        }

    }

    private Vector3 GetSpawnPosition()
    {
        //var player = GameManager.Instance.Player;

        var n = Mathf.Pow(-1, Random.Range(0, 2));

        //Vector3 spawnPos = player.transform.position;
        Vector3 spawnPos = Vector3.zero;

        spawnPos.y = 1;

        if (n == -1)
        { // horizon
            spawnPos.x += Random.Range(-23f, 23f);
            spawnPos.z += 23f * Mathf.Pow(-1, Random.Range(0, 2));
        }
        if (n == 1)
        { // vertical
            spawnPos.x += 23f * Mathf.Pow(-1, Random.Range(0, 2));
            spawnPos.z += Random.Range(-23f, 23f);
        }
        return spawnPos;
    }
}
