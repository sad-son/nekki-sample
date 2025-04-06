using CharacterAssembly;
using PoolAssembly;
using UnityEngine;

namespace AbilitiesAssembly.Projectiles
{
    public class Projectile : PoolObject
    {
        [field: SerializeField] public ProjectileType Type { get; private set; }
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        private Vector3 _target;
        private Vector3 _startPosition;
        private float _timeAlive;
        private ProjectileParameters _projectileParameters;
        
        public void SetTarget(ProjectileParameters projectileParameters)
        {
            _projectileParameters = projectileParameters;
            _timeAlive = 0;
            _startPosition = transform.position;
            _target = _projectileParameters.Target;
        }

        private void FixedUpdate()
        {
            _timeAlive += Time.fixedDeltaTime;
            
            if (_timeAlive >= _projectileParameters.LifeTime)
            {
                ReturnToPool();
                return;
            }
            float distanceTraveled = Vector3.Distance(_startPosition, transform.position);
            if (distanceTraveled >= _projectileParameters.MaxDistance)
            {
                ReturnToPool();
                return;
            }
            
            Rigidbody.position = Vector3.MoveTowards(transform.position, _target, _projectileParameters.Speed * Time.fixedDeltaTime);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.ReceiveDamage(_projectileParameters.Damage);
            }
        }
    }
}