using System.Collections.Generic;
using GameplayDependencies;
using PoolAssembly;
using UnityEngine;

namespace AbilitiesAssembly.Aoe
{
    public class AoeSpawner : IGameplayDependency
    {
        public const int DEFAULT_POOL_SIZE = 10;
            
        private readonly Dictionary<AoeType, PoolManager<AoeArea>> _poolManagers = new();
        
        private readonly AoeConfig _aoeConfig;
        private readonly Transform _projectilesRoot;
        
        public AoeSpawner(AoeConfig aoeConfig, Transform projectilesRoot)
        {
            _projectilesRoot = projectilesRoot;
            _aoeConfig = aoeConfig;

            foreach (var projectileKvp in _aoeConfig.GetDictionary())
            {
                var prefab = projectileKvp.Value;
                CreatePool(projectileKvp.Key, prefab);
            }
        }
        
        public void Dispose()
        {
            foreach (var kvp in _poolManagers)
            {
                kvp.Value.Dispose();
            }
            _poolManagers.Clear();
        }
        
        private void CreatePool(AoeType type, AoeArea prefab)
        {
            _poolManagers[type] = new PoolManager<AoeArea>(() =>
            {
                return Object.Instantiate(prefab, _projectilesRoot);
            });
            
            _poolManagers[type].CreateDefaultInstances(DEFAULT_POOL_SIZE);
        }
        
        public AoeArea Spawn(AoeType type)
        {
            if (!_poolManagers.TryGetValue(type, out var pool))
            {
                Debug.LogError($"No projectiles pool available for {type}");
            }
            
            return pool?.PopOrCreate();
        }
    }
}