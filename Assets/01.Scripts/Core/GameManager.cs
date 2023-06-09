using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private PoolingListSO _poolingList;

    [SerializeField]
    private Transform _spawnPointParent;

    [SerializeField]
    private SpawnListSO _spawnList;

    private float[] _spawnWeights;

    [SerializeField]
    private Transform _playerTrm; //이건 나중에 찾아오는 형식으로 변경해야 함.
    public Transform PlayerTrm => _playerTrm;

    private List<Transform> _spawnPointList;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is running! Check!");
        }
        Instance = this;

        TimeController.Instance = transform.AddComponent<TimeController>();

        MakePool();
        _spawnPointList = new List<Transform>();
        _spawnPointParent.GetComponentsInChildren<Transform>(_spawnPointList);
        _spawnPointList.RemoveAt(0);


        _spawnWeights = _spawnList.SpawnPairs.Select(s => s.spawnPercent).ToArray();
    }


    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform); //풀매니저 만들어주고
        _poolingList.list.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));

    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        float currentTime = 0;
        while (true)
        {
            currentTime += Time.deltaTime;
            //시퀀스를 줘서 한 4~6마리가 스폰포인트 반경 2유닛의 범위에서
            //순차적으로 나오도록 만드는 거지
            if (currentTime >= 5f)
            {
                currentTime = 0;
                int idx = Random.Range(0, _spawnPointList.Count);

                int cnt = Random.Range(3, 7);
                for (int i = 0; i < cnt; i++)
                {
                    int sIndex = GetRandomSpawnIndex();

                    EnemyBrain enemy = PoolManager.Instance.Pop(_spawnList.SpawnPairs[sIndex].prefab.name) as EnemyBrain;
                    Vector2 positionOffset = Random.insideUnitCircle * 2;

                    enemy.transform.position = _spawnPointList[idx].position + (Vector3)positionOffset;
                    enemy.ShowEnemy();
                    float showTime = Random.Range(0.1f, 0.3f);
                    yield return new WaitForSeconds(showTime);
                }
            }
            yield return null;
        }
    }
    private int GetRandomSpawnIndex()
    {
        float sum = 0f;
        for (int i = 0; i < _spawnWeights.Length; i++)
        {
            sum += _spawnWeights[i];
        }
        float randomValue = Random.Range(0f, sum);
        float tempSum = 0;

        for (int i = 0; i < _spawnWeights.Length; i++)
        {
            if (randomValue >= tempSum && randomValue < tempSum + _spawnWeights[i])
            {
                return i;
            }
            else
            {
                tempSum += _spawnWeights[i];
            }
        }
        return 0;
    }
}
