using UnityEngine;

namespace CharacterAssembly
{
    public class Character : MonoBehaviour
    {
        [field: SerializeField] public Transform Aim { get; private set; }
    }
}