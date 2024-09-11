using UnityEngine;

namespace Services.Interfaces
{
    public interface IRayCastController
    {
        public void Configure(IPlayerMediator mediator, Transform playerTransform);
        public void LookForGround();
    }
}