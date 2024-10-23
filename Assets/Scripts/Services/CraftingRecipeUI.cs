using System.Collections.Generic;
using Gameplay.Items;
using Gameplay.Items.Crafting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class CraftingRecipeUI : MonoBehaviour
    {
        [SerializeField] private List<CraftingIngredientUI> craftingIngredientsUI;
        [SerializeField] private CraftingIngredientUI craftingResult;
        [SerializeField] private TextMeshProUGUI craftedItemName;

        public void Configure(CraftingRecipe recipe)
        {
            for (var index = 0; index < craftingIngredientsUI.Count; index++)
            {
                var craftingIngredientUI = craftingIngredientsUI[index];
                if (index >= recipe.craftingIngredients.Count)
                {
                    craftingIngredientUI.gameObject.SetActive(false);
                    continue;
                }

                var craftingIngredient = recipe.craftingIngredients[index];
                craftingIngredientUI.gameObject.SetActive(true);
                craftingIngredientUI.ingredientImage.sprite = craftingIngredient.Item.sprite;
                craftingIngredientUI.neededQuantityText.text = craftingIngredient.quantity.ToString();
                craftingResult.ingredientImage.sprite = recipe.CraftedItem.sprite;
                craftingResult.neededQuantityText.text = recipe.craftedQuantity.ToString();
                craftedItemName.text = recipe.CraftedItem.itemName;
            }
            gameObject.SetActive(true);
        }
    }
}