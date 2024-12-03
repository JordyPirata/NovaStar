using System;
using System.Collections;
using Services.Interfaces;
using UnityEngine;

namespace Services.Player
{
    public class TemperatureService : StatService, ITemperatureService
    {
        [SerializeField] private float secondsToCheckAgain = 5;

        public int Temperature
        {
            get => Stat;
            set => Stat = value;
        }

        public void MapLoaded()
        {
            StartCoroutine(StartCheckingTemperature());
        }

        private IEnumerator StartCheckingTemperature()
        {
            yield return new WaitForSeconds(secondsToCheckAgain);
            var mapTemperature = ServiceLocator.GetService<IPlayerInfo>().MapTemperature();
            Temperature = (int)((mapTemperature + 10) * 7 / 50) + 34;
            Debug.Log($"CorporalTemperature: {Temperature}");
            StartCoroutine(StartCheckingTemperature());
        }
    }
}
