using UnityEngine;

namespace CharacterAssembly.Animation
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void Idle()
        {
            _animator.SetBool(AnimatorHashes.Walk, false);
            _animator.SetBool(AnimatorHashes.Run, false);
            _animator.SetBool(AnimatorHashes.Idle, true);
        }
        
        public void Walk()
        {
            _animator.SetBool(AnimatorHashes.Run, false);
            _animator.SetBool(AnimatorHashes.Idle, false);
            _animator.SetBool(AnimatorHashes.Walk, true);
        }
        
        public void Run()
        {
            _animator.SetBool(AnimatorHashes.Idle, false);
            _animator.SetBool(AnimatorHashes.Walk, false);
            _animator.SetBool(AnimatorHashes.Run, true);
        }
    }
}