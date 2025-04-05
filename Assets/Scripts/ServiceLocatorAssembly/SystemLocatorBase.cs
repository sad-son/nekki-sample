using System;
using System.Collections.Generic;

namespace ServiceLocatorSystem
{
    public abstract class SystemLocatorBase<TQueryType> : IDisposable, IServiceLocator
    {
        private readonly Dictionary<Type, IDisposable> _services = new();

        public virtual void Dispose()
        {
            foreach (var service in _services)
            {
                service.Value.Dispose();
            }
            
            _services.Clear();
        }
        
        private void RegisterTypesInternal()
        {
            RegisterTypes();
        }
        
        protected abstract void RegisterTypes();
        
        public T ResolveDependency<T>() where T : TQueryType
        {
            return (T)_services[typeof(T)];
        }
        
        protected void Register<T>(T instance) where T : TQueryType, IDisposable
        {
            _services[typeof(T)] = instance;
        }

        public void Register()
        {
            RegisterTypesInternal();
        }
    }
}