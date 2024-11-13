using System;
using System.Collections;
using UnityEngine;

namespace Services.Player
{
    public class DropsService : MonoBehaviour, IDropsService
    {
        [SerializeField] private float secondsToSpawn;

        /*private void Awake()
        {
            StartCoroutine(StartSpawning());
        }

        private IEnumerator StartSpawning()
        {
            yield return new WaitForSeconds(secondsToSpawn);
            /*SpawnDrop();#1#
            StartCoroutine(StartSpawning());
        }*/
    }
}