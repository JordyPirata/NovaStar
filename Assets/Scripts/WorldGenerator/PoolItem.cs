using System.Diagnostics.Tracing;
using System.Threading;
using UnityEngine;

namespace Generator
{
    public class PoolItem
    {
        public GameObject GameObject { get; set; }
        private bool _isAvailable;
        private Mutex mutex = new Mutex();
        public PoolItem(GameObject gameObject)
        {
            GameObject = gameObject;
            _isAvailable = true;
        }

        public PoolItem TryUse()
        {
            if (mutex.WaitOne(0))
            {
                if (_isAvailable)
                {
                    _isAvailable = false;
                    mutex.ReleaseMutex();
                    return this;
                }
                mutex.ReleaseMutex();
            }
            return null;
        }

        public void Release()
        {
            mutex.WaitOne();
            _isAvailable = true;
            mutex.ReleaseMutex();
        }
    }
}