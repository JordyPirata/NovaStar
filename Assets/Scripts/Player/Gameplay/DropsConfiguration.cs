using System.Collections.Generic;
using System.Linq;
using Gameplay.Items;
using Services.Player;
using UnityEngine;

namespace Player.Gameplay
{
    [CreateAssetMenu(menuName = "NovaStar/DropsConfiguration", fileName = "DropsConfiguration")]
    public class DropsConfiguration : ScriptableObject
    {
        public List<DropsByRarity> dropsByRarities;

        public int GetRandomItemIndexByRarity(ItemRarity itemRarity)
        {
            foreach (var drops in dropsByRarities.Where(drops => drops.rarity == itemRarity))
            {
                return drops.drops[Random.Range(0, drops.drops.Count)];
            }

            Debug.LogError($"something Went Wrong Trying To Get Any item of the rarity {itemRarity}");
            return 0;
        }
    }
}