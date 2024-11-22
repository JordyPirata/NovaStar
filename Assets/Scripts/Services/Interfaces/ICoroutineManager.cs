using System.Collections;

namespace Services.Interfaces
{
    public interface ICoroutineManager
    {
        void EnqueueCoroutine(IEnumerator routine);
        void StopThis(IEnumerator routine);
        void StopAll();
    }
}