using UnityEngine;

namespace AbilitiesAssembly.Projectiles
{
    public struct ProjectileParameters
    {
        public Vector3 Target { get; private set; }
        public float LifeTime { get; private set; }
        public float MaxDistance { get; private set; }
        public float Speed { get; private set; }
        public float Damage { get; private set; }
        
        public ProjectileParameters(float lifeTime, float maxDistance, float speed, float damage, Vector3 target)
        {
            Target = target;
            LifeTime = lifeTime;
            MaxDistance = maxDistance;
            Speed = speed;
            Damage = damage;
        }
    }
}