using System;
using SpawnerAssembly;
using UnityEngine;
using UnityEngine.Pool;

namespace PoolAssembly
{
    public class PoolManager<T> where T : PoolObject, IDisposable
    {
        private readonly UnityObjectPool<T> _pool;
        private readonly Func<T> _createObjectAction;

        public PoolManager(Func<T> createObjectAction, int defaultCapacity = 10, int maxSize = 10000)
        {
            _createObjectAction = createObjectAction;
            _pool = new UnityObjectPool<T>(CreateInstance, OnPopObject, OnPushObject, OnDestroyObject, defaultCapacity,
                maxSize);
        }

        public void Dispose()
        {
            _pool.Dispose();
        }

        public T PopOrCreate()
        {
            T poolObject;
            bool isDestroyed;
            do
            {
                poolObject = _pool.Get();
                isDestroyed = poolObject == null;
                if (isDestroyed) OnDestroyObject(poolObject);
            } while (isDestroyed);

            return poolObject;
        }

        public void CreateDefaultInstances(int startCount)
        {
            _pool.EnsureInstancesCount(startCount);
        }
        
        private T CreateInstance()
        {
            var obj = _createObjectAction.Invoke();
            return obj;
        }

        private void OnPopObject(T poolObject)
        {
            if (!poolObject) return;
            poolObject.OnPop();
        }

        private void OnPushObject(T poolObject)
        {
            if (!poolObject) return;
            poolObject.OnPush();
            _pool.Release(poolObject);
        }

        private void OnDestroyObject(T poolObject)
        {
            _pool.RemoveDestroyed(poolObject);
        }
    }
}