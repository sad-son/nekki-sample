using System;
using PoolAssembly;
using UnityEngine;

namespace SpawnerAssembly
{
    public class Enemy : PoolObject
    {
        [SerializeField] private GameObject _selector;

        private void Awake()
        {
            Unselect();
        }

        public void Select()
        {
            _selector.gameObject.SetActive(true);
        }

        public void Unselect()
        { 
            _selector.gameObject.SetActive(false);
        }
    }
}