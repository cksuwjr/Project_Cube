using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PoolManager : Singleton<PoolManager>, IManager
{
    public Pool enemyPool;
    public Pool bossPool;

    public void Init()
    {
        enemyPool.Init(Addressables.LoadAssetAsync<GameObject>("Enemy").WaitForCompletion());
        bossPool.Init(Addressables.LoadAssetAsync<GameObject>("Boss").WaitForCompletion());
    }
}
