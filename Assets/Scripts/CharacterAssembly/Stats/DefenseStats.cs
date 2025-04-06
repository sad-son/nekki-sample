using System;
using UnityEngine;

namespace CharacterAssembly.Stats
{
    [Serializable]
    public class DefenseStats : IDisposable
    {
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Armor { get; private set; }
        
        public event Action OnDeath;

        public DefenseStats(float health, float armor, Action deathCallback)
        {
            MaxHealth = health;
            Health = health;
            Armor = armor;
            OnDeath = deathCallback;
        }

        public void Dispose()
        {
            OnDeath = null;
        }
        public void Hit(float damage)
        {
            var accumulatedDamage = damage - (damage * Armor);
            Health -= accumulatedDamage;
            if (Health <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}