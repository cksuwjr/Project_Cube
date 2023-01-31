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

    [SerializeField] private List<Vector4> SpawnMobCount;
    public GameObject danger;

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
            StartCoroutine(NextStage(1));
            //StartCoroutine(Spawn(Enemy, (int)SpawnMobCount[Stage].x));
            //StartCoroutine(Spawn(Boss, (int)SpawnMobCount[Stage].y));
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
            Spawned.GetComponent<Status>().MaxHp += ((Stage - 4) * 150);
            Spawned.GetComponent<Status>().Hp = Spawned.GetComponent<Status>().MaxHp;
            if (Monster == Boss)
            {
                if ((Stage - 4) > 0)
                {
                    if ((Stage - 4) < 5)
                    {
                        Spawned.GetComponent<CubeController>().Skill_Q.skill_Level = Stage - 4;
                        Spawned.GetComponent<CubeController>().Skill_W.skill_Level = Stage - 4;
                        Spawned.GetComponent<CubeController>().Skill_E.skill_Level = Stage - 4;
                        Spawned.GetComponent<CubeController>().Skill_R.skill_Level = Stage - 4;
                    }
                    else
                    {
                        Spawned.GetComponent<CubeController>().Skill_Q.skill_Level = 5;
                        Spawned.GetComponent<CubeController>().Skill_W.skill_Level = 5;
                        Spawned.GetComponent<CubeController>().Skill_E.skill_Level = 5;
                        Spawned.GetComponent<CubeController>().Skill_R.skill_Level = 5;
                    }
                }
                else {
                    Spawned.GetComponent<CubeController>().Skill_Q.skill_Level = 1;
                    Spawned.GetComponent<CubeController>().Skill_W.skill_Level = 1;
                    Spawned.GetComponent<CubeController>().Skill_E.skill_Level = 1;
                    Spawned.GetComponent<CubeController>().Skill_R.skill_Level = 1;
                }
            }



            Spawned.SetActive(true);
            count++;
            yield return new WaitForSeconds(SpawnTerm);
        }
        if (Monster == Enemy)
            isAllSpawned_Enemy = true;
        if (Monster == Boss)
            isAllSpawned_Boss = true;

        if (isAllSpawned_Enemy && isAllSpawned_Boss && cubeUI) { 
            StartCoroutine(NextStage(10 + Stage));
        }
    }
    IEnumerator NextStage(float time)
    {
        yield return new WaitForSeconds(time);

        if (Stage < SpawnMobCount.Count)
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
