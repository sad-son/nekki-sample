using ServiceLocatorAssembly;

namespace InputAssembly
{
    public class InputSystemContainer : SystemLocatorBase<IInputSystemDependency>
    {
        protected override void RegisterTypes()
        {
            Register(new InputExecutor());
        }
    }
}