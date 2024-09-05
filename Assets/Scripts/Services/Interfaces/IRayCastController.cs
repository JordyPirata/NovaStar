using UnityEngine;

namespace Services.Interfaces
{
    public interface IRayCastController
    {
        public void Configure(IPlayerMediator playerMediator, Transform transform);
        public void LookForGround();
    }
}