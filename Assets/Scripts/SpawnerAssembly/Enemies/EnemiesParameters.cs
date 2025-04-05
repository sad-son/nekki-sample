using System;
using UnityEngine;

namespace SpawnerAssembly
{
    [Serializable]
    public class EnemiesParameters
    {
        [field: SerializeField] public EnemiesType EnemiesType { get; private set; }
        [field: SerializeField] public Enemy EnemyPrefab { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Armor { get; private set; }
        [field: SerializeField] public int PoolSize { get; private set; }
    }
}