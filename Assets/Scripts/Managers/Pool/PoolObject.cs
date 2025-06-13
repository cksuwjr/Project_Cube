using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private Pool pool;

    public void Init(Pool myPool)
    {
        pool = myPool;
        gameObject.SetActive(true);
    }

    public void ReturnToPool()
    {
        pool.ReturnPoolObject(this);
    }
}