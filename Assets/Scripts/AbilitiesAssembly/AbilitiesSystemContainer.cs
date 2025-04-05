using ServiceLocatorSystem;

namespace AbilitiesAssembly
{
    public class AbilitiesSystemContainer : SystemLocatorBase<IAbilitySystemDependency>
    {
        protected override void RegisterTypes()
        {
            Register(new TargetSelector());
        }
    }
}