using System.Collections;
using Gameplay.Items;
using Player.Gameplay;
using Services.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services.Player
{
    public class DropsService : MonoBehaviour, IDropsService
    {
        [SerializeField] private float secondsToSpawn;
        [SerializeField] private Drop dropPrefab;
        [SerializeField] private DropsConfiguration dropsConfiguration;
        [SerializeField] private DropUIWindow dropUIWindow;
        private int _dropSpaces = 10;

        private void Awake()
        {
            StartCoroutine(StartSpawning());
        }

        private IEnumerator StartSpawning()
        {
            SpawnDrop();
            yield return new WaitForSeconds(10);
            StartCoroutine(StartSpawning());
        }

        private void SpawnDrop()
        {
            var dropPosition = GetDropPosition();
            var itemsAmount = Random.Range(0, _dropSpaces);
            var drop = Instantiate(dropPrefab, dropPosition, Quaternion.identity, transform);
            var dropItemsData = new int[itemsAmount];
            for (int i = 0; i < itemsAmount; i++)
            {
                var itemRarityPercentage = Random.Range(0, 100);
                var itemRarity = ItemRarity.Common;
                switch (itemRarityPercentage)
                {
                    case <= 1: itemRarity = ItemRarity.Epic; break;
                    case <= 5: itemRarity = ItemRarity.VeryRare; break;
                    case <= 31: itemRarity = ItemRarity.Rare; break;
                    case <= 100: break;
                    default: Debug.LogError($"got an error trying get a random {itemRarityPercentage}"); break;
                }

                dropItemsData[i] = dropsConfiguration.GetRandomItemIndexByRarity(itemRarity);
            }
            drop.Configure(dropItemsData, dropUIWindow);
        }

        private Vector3 GetDropPosition()
        {
            return Vector3.zero;
        }
    }
}