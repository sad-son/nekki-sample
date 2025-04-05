﻿using System.Collections.Generic;
using GameplayDependencies;
using PoolAssembly;
using UnityEngine;

namespace SpawnerAssembly
{
    public class EnemiesSpawner : IGameplayDependency
    {
        private readonly EnemiesConfig _enemiesConfig;
        private readonly Transform _enemiesRoot;
        private readonly Dictionary<EnemiesType, EnemiesParameters> _enemiesParametersDictionary;
        private readonly Dictionary<EnemiesType, PoolManager<Enemy>> _enemiesPoolDictionary = new();
        
        public EnemiesSpawner(EnemiesConfig enemiesConfig, Transform enemiesRoot)
        {
            _enemiesRoot = enemiesRoot;
            _enemiesConfig = enemiesConfig;
            _enemiesParametersDictionary = _enemiesConfig.GetParametersDictionary();
            WarmUp();
        }

        private void WarmUp()
        {
            foreach (var enemyKvp in _enemiesParametersDictionary)
            {
                var parameters = enemyKvp.Value;
                var pool = new PoolManager<Enemy>(() => Object.Instantiate(parameters.EnemyPrefab, _enemiesRoot));
                pool.CreateDefaultInstances(parameters.PoolSize);

                _enemiesPoolDictionary[enemyKvp.Key] = pool;
            }
        }
        
        public Enemy Spawn(EnemiesType enemiesType)
        {
            if (!_enemiesPoolDictionary.TryGetValue(enemiesType, out var pool))
            {
                Debug.LogError($"No enemies pool available for {enemiesType}");
            }
            
            return pool?.PopOrCreate();
        }
        
        public void Dispose()
        {
            _enemiesParametersDictionary?.Clear();
            _enemiesPoolDictionary?.Clear();
        }
    }
}