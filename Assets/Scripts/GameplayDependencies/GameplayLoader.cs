using System.Collections;
using System.Collections.Generic;
using AbilitiesAssembly;
using CameraAssembly;
using InputAssembly;
using ServiceLocatorSystem;
using SpawnerAssembly;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameplayDependencies
{
    public class GameplayLoader : MonoBehaviour
    {
        [SerializeField] private TopDownCameraController _cameraController;
        [SerializeField] private EnemiesConfig _enemiesConfig;
        [SerializeField] private Transform _enemiesRoot;
        
        [SerializeField] private GameObject _characterPrefab;
        [SerializeField] private Transform _characterSpawnPoint;
        [SerializeField] private List<EnemySpawnPoint> _enemySpawnPoints = new();
        
        private void OnValidate()
        {
            Assert.IsNotNull(_cameraController);
            Assert.IsNotNull(_characterPrefab);
            Assert.IsNotNull(_characterSpawnPoint);
        }

        private IEnumerator Start()
        {
            ServiceLocatorController.Register(new InputSystemContainer());
            ServiceLocatorController.Register(new SpawnerContainer(_characterPrefab, _characterSpawnPoint, _enemiesConfig, _enemiesRoot));
            
            ServiceLocatorController.Resolve<SpawnerContainer>().Resolve<MyCharacterDependency>().Spawn();
            
            yield return null;
            ServiceLocatorController.Register(new GameplaySystemContainer(_cameraController));
            ServiceLocatorController.Register(new AbilitiesSystemContainer());

            foreach (var enemySpawnPoint in _enemySpawnPoints)
            {
                enemySpawnPoint.Spawn();
            }
        }

        public void CollectSpawnPoint(EnemySpawnPoint[] enemySpawnPoints)
        {
            _enemySpawnPoints.Clear();
            _enemySpawnPoints.AddRange(enemySpawnPoints);
        }
    }
}