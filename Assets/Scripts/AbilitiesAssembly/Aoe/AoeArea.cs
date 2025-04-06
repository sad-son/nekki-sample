using System;
using PoolAssembly;
using UnityEngine;

namespace AbilitiesAssembly.Aoe
{
    public class AoeArea : PoolObject
    {
        [field: SerializeField] public AoeType Type { get; private set; }

        private AoeParameters _aoeParameters;
        private float _timeAlive;
        
        public void Setup(AoeParameters aoeParameters)
        {
            _timeAlive = 0;
            _aoeParameters = aoeParameters;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.ReceiveDamage(_aoeParameters.AoeAbilityParameters.Damage);
            }
        }

        private void FixedUpdate()
        {
            _timeAlive += Time.fixedDeltaTime;
            
            if (_timeAlive >= _aoeParameters.AoeAbilityParameters.LifeTime)
            {
                ReturnToPool();
                return;
            }
        }
    }
}