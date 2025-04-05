using CharacterAssembly;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.AI;

namespace SpawnerAssembly
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveToCharacter : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        private MyCharacterDependency _characterDependency;
        private Rigidbody _rigidbody;

        public void Setup()
        {
            _characterDependency = ServiceLocatorController.Resolve<SpawnerContainer>().ResolveDependency<MyCharacterDependency>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _navMeshAgent.enabled = false;
            Run();
        }

        private void OnEnable()
        {
            Run();
        }

        private void OnDisable()
        {
            _navMeshAgent.enabled = false;
        }

        private void Run()
        {
            _animator.SetBool(AnimatorHashes.Run, true);
            _navMeshAgent.enabled = true;
        }
        
        private void FixedUpdate()
        {
            _navMeshAgent.SetDestination(_characterDependency.Character.transform.position);
            SetRotation(_characterDependency.Character.transform.position, _rotationSpeed * Time.fixedDeltaTime);
        }

        private void SetRotation(Vector3 targetPosition, float time)
        {
            var directionToPlayer = (targetPosition - transform.position).normalized;
            var targetRotation = Quaternion.LookRotation(directionToPlayer);
            var newRotation = Quaternion.Slerp(_rigidbody.rotation, targetRotation, time);

            _rigidbody.MoveRotation(newRotation);
        }
    }
}