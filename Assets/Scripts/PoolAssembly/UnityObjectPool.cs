using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace PoolAssembly
{
    public class UnityObjectPool<T> : IDisposable, IObjectPool<T> where T : class
    {
        private readonly List<T> _cache;
        private readonly List<T> _active;
        private readonly Func<T> _createFunc;
        private readonly Action<T> _actionOnGet;
        private readonly Action<T> _actionOnRelease;
        private readonly Action<T> _actionOnDestroy;

        private int _maxSize;
        private int _recycleIndex;

        public int CountInactive => _cache.Count;
        public int CountActive => _active.Count;
        public int CountAll => CountInactive + CountActive;

        internal IReadOnlyCollection<T> Cache => _cache;
        internal IReadOnlyCollection<T> Active => _active;
        internal int MaxSize => _maxSize;

        public UnityObjectPool(
            Func<T> createFunc,
            Action<T> actionOnGet = null,
            Action<T> actionOnRelease = null,
            Action<T> actionOnDestroy = null,
            int defaultCapacity = 10,
            int maxSize = 10000)
        {
            if (maxSize <= 0)
                throw new ArgumentException("Max Size must be greater than 0", nameof(maxSize));
            _cache = new List<T>(defaultCapacity);
            _active = new List<T>(defaultCapacity);
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            _maxSize = maxSize;
            _actionOnGet = actionOnGet;
            _actionOnRelease = actionOnRelease;
            _actionOnDestroy = actionOnDestroy;
        }

        public void SetMaxSize(int maxSize) => _maxSize = maxSize;

        public void EnsureInstancesCount(int count)
        {
            var createCount = Mathf.Max(count - CountAll, 0);
            if (createCount > 0) AppendInstances(createCount);
        }

        public void AppendInstances(int count)
        {
            for (var i = 0; i < count && CountAll < _maxSize; i++)
            {
                var obj = _createFunc();
                _cache.Add(obj);
                _actionOnRelease.Invoke(obj);
            }
        }
        
        public T Get()
        {
            T obj;
            if (CountActive >= _maxSize)
                return _active[_recycleIndex = (_recycleIndex + 1) % _active.Count];

            if (_cache.Count == 0)
            {
                obj = _createFunc();
            }
            else
            {
                var index = _cache.Count - 1;
                obj = _cache[index];
                _cache.RemoveAt(index);
            }

            _active.Add(obj);
            _actionOnGet?.Invoke(obj);
            return obj;
        }

        public PooledObject<T> Get(out T v)
        {
            return new PooledObject<T>(v = Get(), this);
        }

        public void Release(T element)
        {
            if (!_active.Remove(element))
                return;

            _actionOnRelease?.Invoke(element);
            if (CountInactive < _maxSize)
            {
                _cache.Add(element);
            }
            else
            {
                _actionOnDestroy?.Invoke(element);
            }
        }

        public void Clear()
        {
            if (_actionOnDestroy != null)
            {
                foreach (T obj in _cache)
                    _actionOnDestroy(obj);
                foreach (T obj in _active)
                    _actionOnDestroy(obj);
            }

            _cache.Clear();
            _active.Clear();
        }

        public void RemoveDestroyed(T element)
        {
            _cache.Remove(element);
            _active.Remove(element);
        }

        public void Dispose() => Clear();
    }
}