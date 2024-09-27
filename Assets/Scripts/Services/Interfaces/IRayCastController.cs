using UnityEngine;

namespace Services.Interfaces
{
    public interface IRayCastController
    {
        public void LookForGround(Transform playerTransform);
        public void CheckFall(Transform playerTransform);
    }
}