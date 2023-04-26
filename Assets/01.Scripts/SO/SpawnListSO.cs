using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SpawnPair
{
    public PoolableMono prefab;
    public float spawnPercent;
}

[CreateAssetMenu(menuName = "SO/SpawnList")]
public class SpawnListSO : ScriptableObject
{
    public List<SpawnPair> SpawnPairs;
}
