using System;
using AbilitiesAssembly;
using CharacterAssembly.Stats;
using PoolAssembly;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace SpawnerAssembly
{
    [RequireComponent(typeof(StatsProvider))]
    public class Enemy : PoolObject, IDamageable
    {
        [SerializeField] private StatsProvider _statsProvider;
        [SerializeField] private GameObject _selector;
        [SerializeField] private MoveToCharacter _moveToCharacter;

        public event Action OnDeath;
        
        private EnemiesParameters _enemiesParameters;
        
        private void Awake()
        {
            Unselect();
        }

        public void Setup(EnemiesParameters enemiesParameters)
        {
            _enemiesParameters = enemiesParameters;
            _moveToCharacter.Setup();
            Respawn();
        }

        public void Respawn()
        {
            _statsProvider.Setup(_enemiesParameters.Health, _enemiesParameters.Armor, Death);
        }

        private void Death()
        {
            Unselect();
            ReturnToPool();
            OnDeath?.Invoke();
        }

        public void Select()
        {
            _selector.gameObject.SetActive(true);
        }

        public void Unselect()
        { 
            _selector.gameObject.SetActive(false);
        }

        public void ReceiveDamage(float damage)
        {
            _statsProvider.ReceiveDamage(damage);
        }
    }
}