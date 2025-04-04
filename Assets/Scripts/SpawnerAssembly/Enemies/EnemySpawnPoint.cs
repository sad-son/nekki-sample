﻿using System;
using System.Collections;
using System.Collections.Generic;
using ServiceLocatorSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnerAssembly
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private float _respawnTime = 1f;
        [SerializeField] private List<EnemiesType> _availableEnemies = new();

        private Enemy _currentEnemy;

        public void Spawn()
        {
            if (_availableEnemies == null || _availableEnemies.Count == 0) return;
            
            var randomIndex = Random.Range(0, _availableEnemies.Count);
            var spawnType = _availableEnemies[randomIndex];
            _currentEnemy = ServiceLocatorController.Resolve<SpawnerContainer>().ResolveDependency<EnemiesSpawner>().Spawn(spawnType);
            _currentEnemy.transform.SetPositionAndRotation(transform.position, transform.rotation);
            _currentEnemy.Respawn();
            _currentEnemy.OnDeath += Respawn;
        }

        private void Respawn()
        {
            StartCoroutine(RespawnCoroutine());
        }

        private IEnumerator RespawnCoroutine()
        {
            _currentEnemy.OnDeath -= Respawn;
            yield return new WaitForSeconds(_respawnTime);
            Spawn();
        }
    }
}