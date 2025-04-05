using System;
using Unity.Collections;
using UnityEngine;

namespace CharacterAssembly.Stats
{
    [Serializable]
    public class DefenseStats
    {
        [field: SerializeField, ReadOnly] public float MaxHealth { get; private set; }
        [field: SerializeField, ReadOnly] public float Health { get; private set; }
        [field: SerializeField, ReadOnly] public float Armor { get; private set; }
        
        public event Action OnDeath;

        public DefenseStats(float health, float armor)
        {
            MaxHealth = health;
            Health = health;
            Armor = armor;
        }
        
        public void Hit(float damage)
        {
            Health -= damage - (damage * Armor);
            if (Health <= 0) 
                OnDeath?.Invoke();
        }
    }
}