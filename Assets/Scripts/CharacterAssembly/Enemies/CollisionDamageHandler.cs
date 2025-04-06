using CharacterAssembly;
using Helpers;
using UnityEngine;

namespace SpawnerAssembly
{
    public class CollisionDamageHandler : MonoBehaviour
    {
        [SerializeField] private float _damage;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(TagsHelper.PlayerTag) && other.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.ReceiveDamage(_damage);
            }
        }
    }
}