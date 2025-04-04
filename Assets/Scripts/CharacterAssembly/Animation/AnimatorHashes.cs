using UnityEngine;

namespace CharacterAssembly
{
    public static class AnimatorHashes
    {
        public static readonly int Idle = Animator.StringToHash("Idle");
        public static readonly int Walk = Animator.StringToHash("Walk");
        public static readonly int Run = Animator.StringToHash("Run");
    }
}