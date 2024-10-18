using Gameplay.Items.Crafting;
using UnityEngine;

namespace Services
{
    public class CraftingService : MonoBehaviour, ICraftingService
    {
        [SerializeField] private CraftingRecipesData craftingRecipesData;
    }
}