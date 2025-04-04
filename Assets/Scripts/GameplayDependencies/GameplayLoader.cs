using System.Collections;
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
        [SerializeField] private GameObject _characterPrefab;
        [SerializeField] private Transform _characterSpawnPoint;
        
        private void OnValidate()
        {
            Assert.IsNotNull(_cameraController);
            Assert.IsNotNull(_characterPrefab);
            Assert.IsNotNull(_characterSpawnPoint);
        }

        private IEnumerator Start()
        {
            ServiceLocatorController.Register(new InputSystemContainer());
            ServiceLocatorController.Register(new SpawnerContainer(_characterPrefab, _characterSpawnPoint));
            
            ServiceLocatorController.Resolve<SpawnerContainer>().Resolve<MyCharacterDependency>().Spawn();
            
            yield return null;
            ServiceLocatorController.Register(new GameplaySystemContainer(_cameraController));
        }
    }
}