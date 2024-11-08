using UnityEngine;

namespace Services.Interfaces
{
    public interface IInteractionService
    {
        void Configure(float interactionDistance, LayerMask interactionLayer);
        void SetCamera(Camera mainCamera);
        void CanGetItems(bool canGetItems);
    }
}