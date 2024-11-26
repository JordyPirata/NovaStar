using System;
using Gameplay.Items.Crafting;
using Player.Gameplay.UserInterface;
using Services.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Services
{
    public class CraftingService : MonoBehaviour, ICraftingService
    {
        [SerializeField] private CraftingRecipesData craftingRecipesData;
        [SerializeField] private CraftingRecipeUI craftingRecipeUITemplate;

        private void Start()
        {
            craftingRecipeUITemplate.gameObject.SetActive(false);

            foreach (var recipe in craftingRecipesData.craftingRecipes)
            {
                var recipeInstance = Instantiate(craftingRecipeUITemplate, craftingRecipeUITemplate.gameObject.transform.parent);
                recipeInstance.Configure(recipe, ref ServiceLocator.GetService<IInventoryService>().GetOnInventoryUpdated());
            }
        }
    }
}