using System.Collections.Generic;
using CharacterAssembly.Animation;
using CharacterAssembly.Movement.Movers;
using InputAssembly;
using ServiceLocatorAssembly;
using UnityEngine;
using UnityEngine.Assertions;

namespace CharacterAssembly.Movement
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(AnimatorController))]
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private MovementConfig _movementConfig;
        
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public AnimatorController AnimatorController { get; private set; }
        
        private readonly Dictionary<MoverType, IMover> _moversDictionary = new();

        private Dictionary<MoverType, MovementSettings> _moversSettingsDictionary = new();
        private IMover _currentMover;
        private InputExecutor _inputExecutor;
        
        private void OnValidate()
        {
            Assert.IsNotNull(_movementConfig);
        }
        
        private void Awake()
        {
            _inputExecutor = ServiceLocatorController.Resolve<InputSystemContainer>().ResolveDependency<InputExecutor>();
            _moversSettingsDictionary = _movementConfig.GetSettingsDictionary();

            _moversDictionary.Add(MoverType.Idle, new IdleMover(this, _moversSettingsDictionary[MoverType.Idle]));
            _moversDictionary.Add(MoverType.Walk, new WalkMover(this, _moversSettingsDictionary[MoverType.Walk]));
            _moversDictionary.Add(MoverType.Run, new RunMover(this, _moversSettingsDictionary[MoverType.Run]));

            SetMover(MoverType.Idle);
        }
        
        public void SetMover(MoverType moverType)
        {
            var newMover = _moversDictionary[moverType];
            if (newMover != null && _currentMover == newMover) return;

            _currentMover?.Exit();
            _currentMover = newMover;
            _currentMover?.Enter();
        }

        private void FixedUpdate()
        {
            var moveInput = _inputExecutor.MoveInput;
            var movementDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            if (movementDirection == Vector3.zero)
            {
                SetMover(MoverType.Idle);
                return;
            }

            if (_inputExecutor.Sprint)
            {
                SetMover(MoverType.Run);
            }
            else
            {
                SetMover(MoverType.Walk);
            }
            _currentMover.Move(Time.fixedDeltaTime, movementDirection);
        }
    }
}