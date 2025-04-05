using System;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitiesAssembly.Projectiles
{
    [CreateAssetMenu(menuName = "Data/" + nameof(ProjectilesConfig), fileName = nameof(ProjectilesConfig))]
    public class ProjectilesConfig : ScriptableObject
    {
        [SerializeField] private List<Projectile> _projectiles;

        public Dictionary<ProjectileType, Projectile> GetDictionary()
        {
            var result = new Dictionary<ProjectileType, Projectile>();
            foreach (var projectile in _projectiles)
            {
                if (!result.TryAdd(projectile.Type, projectile))
                {
                    Debug.LogWarning($"Duplicate setting: {projectile.GetType()}");
                }
            }
            
            return result;
        }
        
    }
}