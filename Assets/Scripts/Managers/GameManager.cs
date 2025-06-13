using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private DataManager dataManager;
    //private UIManager uiManager;
    private PoolManager poolManager;
   
    [SerializeField] private GameObject player;

    public GameObject Player
    {
        get
        {
            if (player == null)
                player = GameObject.Find("Player");
            return player;
        }
    }

    protected override void DoAwake()
    {
        AssignManagers();
        InitManagers();
    }

    private void AssignManagers()
    {
        GameObject.Find("DataManager")?.TryGetComponent<DataManager>(out dataManager);
        //GameObject.Find("SoundManager")?.TryGetComponent<SoundManager>(out soundManager);
        //GameObject.Find("UIManager")?.TryGetComponent<UIManager>(out uiManager);
        GameObject.Find("PoolManager")?.TryGetComponent<PoolManager>(out poolManager);
    }

    private void InitManagers()
    {
        dataManager?.Init();
        //uiManager?.Init();
        poolManager?.Init();
        //soundManager?.Init();
    }
}