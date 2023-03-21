using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private PoolableMono _bulletPrefab;
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
        PoolManager.Instance = new PoolManager(transform); //풀매니저 만들기
        PoolManager.Instance.CreatePool(_bulletPrefab, 20); //총알 풀이 완성
    }
}
