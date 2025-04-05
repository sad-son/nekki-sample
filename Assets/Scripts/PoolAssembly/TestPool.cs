using System;
using SpawnerAssembly;
using UnityEngine;

namespace PoolAssembly
{
    public class TestPool : MonoBehaviour
    {
        [SerializeField] private Enemy prefab;

        private void Awake()
        {
            var pool = new PoolManager<Enemy>(() => Instantiate(prefab, transform));
            pool.CreateDefaultInstances(10);
        }
    }
}