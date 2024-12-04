using System;
using System.Collections;
using Services.Interfaces;
using UnityEngine;

namespace Services.Player
{
    public class TemperatureService : StatService, ITemperatureService
    {
        [SerializeField] private float secondsToCheckAgain = 5;
        private bool _hasDrinkEffect, _hasHat;
        private int _temperatureModification;

        public int Temperature
        {
            get => Stat;
            set => Stat = value;
        }

        public void MapLoaded()
        {
            StartCoroutine(StartCheckingTemperature());
        }

        public void DrinkSomeWater()
        {
            if (_hasDrinkEffect)
            {
                _temperatureModification++;
                StopCoroutine(DrinkSomeWaterCoroutine());   
            }

            StartCoroutine(DrinkSomeWaterCoroutine());
        }

        public void EquipHat()
        {
            _hasHat = true;
        }

        private IEnumerator StartCheckingTemperature()
        {
            yield return new WaitForSeconds(secondsToCheckAgain);
            var mapTemperature = 15f;
            try
            {
                mapTemperature = ServiceLocator.GetService<IPlayerInfo>().MapTemperature();
            }
            catch (Exception e)
            {
                Console.WriteLine(e); 
            }
            Temperature = (int)((mapTemperature + 10) * 7 / 50) + 34 + _temperatureModification;
            Debug.Log($"MapTemperature:{mapTemperature}. CorporalTemperature: {Temperature}");
            StartCoroutine(StartCheckingTemperature());
        }

        private IEnumerator DrinkSomeWaterCoroutine()
        {
            _temperatureModification--;
            _hasDrinkEffect = true;
            yield return new WaitForSeconds(120);
            _temperatureModification++;
            _hasDrinkEffect = false;
        }
    }
}
