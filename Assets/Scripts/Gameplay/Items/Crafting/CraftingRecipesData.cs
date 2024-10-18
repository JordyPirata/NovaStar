using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items.Crafting
{
    [CreateAssetMenu(menuName = "NovaStar/CraftingRecipesData", fileName = "CraftingRecipesData")]
    public class CraftingRecipesData : ScriptableObject
    {
        public List<CraftingRecipe> craftingRecipes;
    }
}