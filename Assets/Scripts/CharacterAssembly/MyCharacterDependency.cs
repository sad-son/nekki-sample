﻿using UnityEngine;

namespace CharacterAssembly
{
    public class MyCharacterDependency : ICharacterDependency
    {
        private readonly Character _characterPrefab;
        private readonly Transform _spawnPoint;
        
        public Character Character { get; private set; }
        
        public MyCharacterDependency(Character characterPrefab, Transform spawnPoint)
        {
            _characterPrefab = characterPrefab;
            _spawnPoint = spawnPoint;
            Spawn();
        }

        public void Spawn()
        {
            var instance = Object.Instantiate(_characterPrefab, _spawnPoint.position, _spawnPoint.rotation);
            Character = instance;
        }
        
        public void Dispose()
        {
            
        }
    }
}