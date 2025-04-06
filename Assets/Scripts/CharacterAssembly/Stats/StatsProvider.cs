using System;
using Plugins.procedural_healthbar_shader.HealthBar.Components;
using UnityEngine;

namespace CharacterAssembly.Stats
{
    public class StatsProvider : MonoBehaviour
    {
        [field: SerializeField] public DefenseStats DefenseStats { get; private set; }
        [SerializeField] private HealthBar _healthBar;
        
        public void Setup(float health, float armor, Action deathCallback)
        {
            DefenseStats = new DefenseStats(health, armor, deathCallback);
            UpdateHealthBar();
        }

        private void OnDestroy()
        {
            DefenseStats?.Dispose();
        }

        public void ReceiveDamage(float damage)
        {
            DefenseStats.Hit(damage);
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            _healthBar.HealthNormalized = DefenseStats.Health / DefenseStats.MaxHealth;
        }
    }
}