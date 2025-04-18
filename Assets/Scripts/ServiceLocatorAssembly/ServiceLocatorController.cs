﻿using System;
using System.Collections.Generic;

namespace ServiceLocatorAssembly
{
    public static class ServiceLocatorController
    {
        public static event Action ServiceLocatorInitialized;
        public static bool Initialized { get; private set; }
        
        private static readonly Dictionary<Type, object> _services = new();
        
        public static bool TryResolve<T>(out T value) where T : class, IServiceLocator
        {
            var type = typeof(T);
            var result = _services.TryGetValue(type, out var obj);
            value = obj as T;
            return result;
        }
        
        public static T Resolve<T>() where T : class, IServiceLocator
        {
            return _services[typeof(T)] as T;
        }
        
        public static void Register<T>(T resolver) where T : IServiceLocator
        {
            _services[typeof(T)] = resolver;
            resolver.Register();
        }
        
        public static void Unregister<T>() where T : IServiceLocator
        {
            if (_services[typeof(T)] is IDisposable disposable)
                disposable.Dispose();
            
            _services.Remove(typeof(T));
        }

        public static void TryWaitDependenciesInjected(Action action)
        {
            if (Initialized)
            {
                action?.Invoke();
            }
            else
            {
                ServiceLocatorInitialized += InitializeHandler;
            }

            return;

            void InitializeHandler()
            {
                action?.Invoke();
                ServiceLocatorInitialized -= InitializeHandler;
            }
        }

        
        public static void Ready()
        {
            Initialized = true;
            ServiceLocatorInitialized?.Invoke();
        }

        public static void Dispose()
        {
            Initialized = false;
            ServiceLocatorInitialized = null;
        }
    }
}