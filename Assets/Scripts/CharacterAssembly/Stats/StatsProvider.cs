using System;
using Plugins.procedural_healthbar_shader.HealthBar.Components;
using UnityEngine;

namespace CharacterAssembly.Stats
{
    public class StatsProvider : MonoBehaviour
    {
        [SerializeField] private DefenseStats _defenseStats;
        [SerializeField] private HealthBar _healthBar;
        
        private void Awake()
        {
           // _defenseStats = new DefenseStats();
        }

        public void ReceiveDamage(float damage)
        {
            _defenseStats.Hit(damage);
            _healthBar.HealthNormalized = _defenseStats.Health / _defenseStats.MaxHealth;
        }
    }
}