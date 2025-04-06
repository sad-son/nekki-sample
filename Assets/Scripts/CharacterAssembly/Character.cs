using CharacterAssembly.Stats;
using UnityEngine;

namespace CharacterAssembly
{
    public class Character : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField,Range(0, 1)] private int _maxArmor = 0;
        [SerializeField] private StatsProvider _statsProvider;
        [field: SerializeField] public Transform Aim { get; private set; }

        private void Awake()
        {
            _statsProvider.Setup(100, _maxArmor, Death);
        }

        private void Death()
        {
            _statsProvider.Setup(100, _maxArmor, Death);
            Debug.LogError($"Character is dead");
        }

        public void ReceiveDamage(float damage)
        {
            _statsProvider.ReceiveDamage(damage);
        }
    }
}