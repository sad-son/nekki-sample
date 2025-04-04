using GameplayDependencies;
using UnityEngine;

namespace SpawnerAssembly
{
    public class MyCharacterDependency : IGameplayDependency
    {
        private readonly GameObject _characterPrefab;
        private readonly Transform _spawnPoint;
        
        public Transform Character { get; private set; }
        
        public MyCharacterDependency(GameObject characterPrefab, Transform spawnPoint)
        {
            _characterPrefab = characterPrefab;
            _spawnPoint = spawnPoint;
        }

        public void Spawn()
        {
            var instance = Object.Instantiate(_characterPrefab, _spawnPoint.position, _spawnPoint.rotation);
            Character = instance.transform;
        }
        
        public void Dispose()
        {
            
        }
    }
}