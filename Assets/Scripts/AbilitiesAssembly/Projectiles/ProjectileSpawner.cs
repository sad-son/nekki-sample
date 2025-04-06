using System;
using System.Collections.Generic;
using GameplayDependencies;
using PoolAssembly;
using UnityEngine;

namespace AbilitiesAssembly.Projectiles
{
    public class ProjectileSpawner : IGameplayDependency
    {
        public const int DEFAULT_POOL_SIZE = 10;
            
        private readonly Dictionary<ProjectileType, PoolManager<Projectile>> _projectilePoolManagers = new();
        
        private readonly ProjectilesConfig _projectilesConfig;
        private readonly Transform _projectilesRoot;
        
        public ProjectileSpawner(ProjectilesConfig projectilesConfig, Transform projectilesRoot)
        {
            _projectilesRoot = projectilesRoot;
            _projectilesConfig = projectilesConfig;

            foreach (var projectileKvp in _projectilesConfig.GetDictionary())
            {
                var prefab = projectileKvp.Value;
                CreatePool(projectileKvp.Key, prefab);
            }
        }
        
        public void Dispose()
        {
            foreach (var kvp in _projectilePoolManagers)
            {
                kvp.Value.Dispose();
            }
            _projectilePoolManagers.Clear();
        }
        
        private void CreatePool(ProjectileType type, Projectile projectilePrefab)
        {
            _projectilePoolManagers[type] = new PoolManager<Projectile>(() =>
            {
                var projectile = UnityEngine.Object.Instantiate(projectilePrefab, _projectilesRoot);
                return projectile;
            });
            
            _projectilePoolManagers[type].CreateDefaultInstances(DEFAULT_POOL_SIZE);
        }
        
        public Projectile Spawn(ProjectileType projectileType)
        {
            if (!_projectilePoolManagers.TryGetValue(projectileType, out var pool))
            {
                Debug.LogError($"No projectiles pool available for {projectileType}");
            }
            
            return pool?.PopOrCreate();
        }
    }
}