using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace CharacterAssembly.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private MovementConfig _movementConfig;
        [SerializeField] private CharacterController _characterController;
        
        private readonly Dictionary<MoverType, IMover> _moversDictionary = new();
        
        private Dictionary<MoverType, MovementSettings> _moversSettingsDictionary = new();
        private IMover _currentMover;
        
        private void OnValidate()
        {
            Assert.IsNotNull(_movementConfig);
        }
        
        private void Awake()
        {
            _moversSettingsDictionary = _movementConfig.GetSettingsDictionary();

            _moversDictionary.Add(MoverType.Walk, new WalkMover(_characterController, _moversSettingsDictionary[MoverType.Walk]));
            _moversDictionary.Add(MoverType.Run, new RunMover(_characterController, _moversSettingsDictionary[MoverType.Run]));

            SetCurrentMover(MoverType.Walk);
        }
        
        private void SetCurrentMover(MoverType moverType)
        {
            var newMover = _moversDictionary[moverType];
            if (newMover != null && _currentMover == newMover) return;

            _currentMover = _moversDictionary[moverType];
        }

        private void FixedUpdate()
        {
            _currentMover.Move(Time.fixedDeltaTime);
        }
    }
}