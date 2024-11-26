using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services.Interfaces;

namespace Services
{
    /// <summary>
    /// This class is responsible for managing the coroutines in the game.
    /// </summary>
    public class CoroutineManager : MonoBehaviour, ICoroutineManager
    {
        
        private readonly Queue<IEnumerator> Coroutines = new Queue<IEnumerator>();
        public void EnqueueCoroutine(IEnumerator routine)
        {
            Coroutines.Enqueue(routine);
        } 
        private void FixedUpdate()
        {
            if (Coroutines.Count == 0) return;
            StartCoroutine(StartNextCoroutine(Coroutines.Dequeue()));
        }
        // if courroutines is not completed start de next courrutine in the next frame
        private IEnumerator StartNextCoroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
            yield return null;
        }
        public void StopAll()
        {
            StopAllCoroutines();
        }
        public void StopThis(IEnumerator routine)
        {
            try
            {
                StopCoroutine(routine);
            }
            catch
            {
                Debug.LogWarning("The coroutine is not running");
            }
        }
    }
}