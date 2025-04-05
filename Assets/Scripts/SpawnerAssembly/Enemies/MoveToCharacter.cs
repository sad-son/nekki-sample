using CharacterAssembly;
using ServiceLocatorSystem;
using UnityEngine;

namespace SpawnerAssembly
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveToCharacter : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private Animator _animator;
        
        private MyCharacterDependency _characterDependency;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _characterDependency = ServiceLocatorController.Resolve<SpawnerContainer>().ResolveDependency<MyCharacterDependency>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Run();
        }

        private void OnEnable()
        {
            Run();
        }

        private void Run()
        {
            _animator.SetBool(AnimatorHashes.Run, true);
        }
        
        private void FixedUpdate()
        {
            _rigidbody.position = Vector3.MoveTowards(transform.position, 
                _characterDependency.Character.transform.position, _moveSpeed * Time.fixedDeltaTime);
            
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