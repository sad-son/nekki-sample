using System.Collections.Generic;
using ServiceLocatorSystem;
using UnityEngine;

namespace SpawnerAssembly
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private List<EnemiesType> _availableEnemies = new();

        public void Spawn()
        {
            if (_availableEnemies == null || _availableEnemies.Count == 0) return;
            
            var randomIndex = Random.Range(0, _availableEnemies.Count);
            var spawnType = _availableEnemies[randomIndex];
            var enemy = ServiceLocatorController.Resolve<SpawnerContainer>().Resolve<EnemiesSpawner>().Spawn(spawnType);
            enemy.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }
}