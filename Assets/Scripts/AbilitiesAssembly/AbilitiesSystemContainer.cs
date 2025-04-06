using ServiceLocatorAssembly;

namespace AbilitiesAssembly
{
    public class AbilitiesSystemContainer : SystemLocatorBase<IAbilitySystemDependency>
    {
        private readonly AbilityConfig _abilityConfig;

        public AbilitiesSystemContainer(AbilityConfig abilityConfig)
        {
            _abilityConfig = abilityConfig;
        }
        
        protected override void RegisterTypes()
        {
            Register(new TargetSelector());
            Register(new AbilityExecutor(_abilityConfig));
        }
    }
}