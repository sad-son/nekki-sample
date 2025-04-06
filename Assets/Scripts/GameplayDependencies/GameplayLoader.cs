using System.Collections;
using System.Collections.Generic;
using AbilitiesAssembly;
using CameraAssembly;
using CharacterAssembly;
using InputAssembly;
using ServiceLocatorAssembly;
using SpawnerAssembly;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameplayDependencies
{
    public class GameplayLoader : MonoBehaviour
    {
        [SerializeField] private TopDownCameraController _cameraController;
        [SerializeField] private CharacterDependencies _characterDependencies;
        [SerializeField] private SpawnDependencies _spawnDependencies;
        [SerializeField] private ConfigsContainer _configsContainer;
        
        [SerializeField] private List<EnemySpawnPoint> _enemySpawnPoints = new();
        
        private void OnValidate()
        {
            Assert.IsNotNull(_cameraController);
            Assert.IsNotNull(_spawnDependencies);
        }

        private IEnumerator Start()
        {
            ServiceLocatorController.Register(new InputSystemContainer());
            ServiceLocatorController.Register(new CharacterContainer(_characterDependencies, _configsContainer.EnemiesConfig));
            ServiceLocatorController.Register(new SpawnerContainer(_spawnDependencies, _configsContainer));
            yield return null;
            ServiceLocatorController.Register(new GameplaySystemContainer(_cameraController));
            ServiceLocatorController.Register(new AbilitiesSystemContainer(_configsContainer.AbilityConfig));

            foreach (var enemySpawnPoint in _enemySpawnPoints)
            {
                enemySpawnPoint.Spawn();
            }
            
            ServiceLocatorController.Ready();
        }

        private void OnDestroy()
        {
            ServiceLocatorController.Dispose();
        }

        public void CollectSpawnPoint(EnemySpawnPoint[] enemySpawnPoints)
        {
            _enemySpawnPoints.Clear();
            _enemySpawnPoints.AddRange(enemySpawnPoints);
        }
    }
}