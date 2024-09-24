using UnityEngine;

namespace Services.Interfaces
{
    public interface IRayCastController
    {
        void Initialize();
        public void LookForGround();
    }
}