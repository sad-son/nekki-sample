using System;
using SpawnerAssembly;
using UnityEngine;

namespace PoolAssembly
{
    public class PoolObject : MonoBehaviour, IDisposable
    {
        public event Action<PoolObject> OnReleased;

        protected virtual void OnDestroy()
        {
            Dispose();
        }

        public virtual void Dispose()
        {
            OnReleased = null;
        }
        
        public virtual void OnPop() 
        {
           gameObject.SetActive(true);
        }

        public void ReturnToPool()
        {
            OnReleased?.Invoke(this);
        }
        
        public virtual void OnPush()
        {
            gameObject.SetActive(false);
        }
    }
}