using System;
using System.Collections.Generic;
using Gameplay.Items;
using Gameplay.Items.Crafting;
using Services.Interfaces;
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
        [SerializeField] private Image uncrafteableImage, uncrafteableImage2;
        [SerializeField] private Button button;
        private Dictionary<string, int> _neededItemsDictionary;

        public void Configure(CraftingRecipe recipe, ref Action<Dictionary<string, int>> onItemDictionaryUpdate)
        {
            onItemDictionaryUpdate += ONItemDictionaryUpdate;
            _neededItemsDictionary = new Dictionary<string, int>();
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
                _neededItemsDictionary.Add(craftingIngredient.Item.itemName, craftingIngredient.quantity);
            }
            craftingResult.ingredientImage.sprite = recipe.CraftedItem.sprite;
            craftingResult.neededQuantityText.text = recipe.craftedQuantity.ToString();
            craftedItemName.text = recipe.CraftedItem.itemName;
            button.onClick.AddListener(() => OnButtonClick(recipe));
            gameObject.SetActive(true);
        }

        private void OnButtonClick(CraftingRecipe recipe)
        {
            ServiceLocator.GetService<IInventoryService>().TryCraftItem(recipe);
        }


        private void ONItemDictionaryUpdate(Dictionary<string, int> ownedItemsDictionary)
        {
            foreach (var neededItem in _neededItemsDictionary)
            {
                var neededItemKey = neededItem.Key;
                if (!ownedItemsDictionary.ContainsKey(neededItemKey))
                {
                    uncrafteableImage.gameObject.SetActive(true);
                    uncrafteableImage.gameObject.SetActive(true);
                    button.enabled = false;
                    return;
                }
                if (ownedItemsDictionary[neededItem.Key] < _neededItemsDictionary[neededItemKey])
                {
                    uncrafteableImage.gameObject.SetActive(true);
                    uncrafteableImage.gameObject.SetActive(true);
                    button.enabled = false;
                    return;
                }
            }
            uncrafteableImage.gameObject.SetActive(false);
            uncrafteableImage.gameObject.SetActive(false);
            button.enabled = true;
        }
    }
}