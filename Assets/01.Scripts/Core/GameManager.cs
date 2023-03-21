using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private PoolingListSO _poolingList;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is Running! Check!");
        }
        Instance = this;

        MakePool();
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform); //Ǯ�Ŵ��� �����
        _poolingList.list.ForEach(p=> PoolManager.Instance.CreatePool(p.prefab, p.poolCount));
    }
}
