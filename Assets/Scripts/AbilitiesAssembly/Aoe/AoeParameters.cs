using AbilitiesAssembly.Parameters;
using UnityEngine;

namespace AbilitiesAssembly.Aoe
{
    public struct AoeParameters
    {
        public AoeAbilityParameters AoeAbilityParameters { get; private set; }
        public Vector3 Target { get; private set; }
        
        public AoeParameters(AoeAbilityParameters aoeAbilityParameters, Vector3 target)
        {
            AoeAbilityParameters = aoeAbilityParameters;
            Target = target;
        }
    }
}