using UnityEngine;
using Services.Interfaces;

namespace Services 
{
    public class CoroutineManagerObject : MonoBehaviour, ICoroutineManager
    {
        public int timePerFrame = 30;

        public void OnEnable ()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.update += Update;	
            #endif
        }

        public void Update () 
        { 
            CoroutineManager.timePerFrame = timePerFrame;
            CoroutineManager.Update();
        }
    }
}