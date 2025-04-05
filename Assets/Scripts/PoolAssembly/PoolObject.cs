using System;
using UnityEngine;

namespace PoolAssembly
{
    public class PoolObject : MonoBehaviour, IDisposable
    {
        public void Dispose()
        {
            
        }
        
        public virtual void OnPop() 
        {
           gameObject.SetActive(true);
        }
        
        public virtual void OnPush()
        {
            gameObject.SetActive(false);
        }
    }
}