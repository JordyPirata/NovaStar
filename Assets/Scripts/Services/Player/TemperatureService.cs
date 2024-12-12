using System;
using System.Collections;
using Services.Interfaces;
using UnityEngine;

namespace Services.Player
{
    public class TemperatureService : StatService, ITemperatureService
    {
        [SerializeField] private float secondsToCheckAgain = 1;
        private bool _hasDrinkEffect, _hasHat, _hasCoat;
        private int _temperatureModification, _lastTemperature, _currentSecondsLapsed;
        private IPlayerMediator _playerMediator;
        private Action _onTemperatureChanged;

        public int Temperature
        {
            get => Stat;
            set => Stat = value;
        }

        private bool Heated => !_hasHat && Temperature == 38;
        private bool OverHeated => Temperature > 38;
        private bool Frozen => Temperature <= 35;

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

        public ITemperatureService Configure(IPlayerMediator playerMediator)
        {
            _playerMediator = playerMediator;
            return this;
        }

        public void EquipCoat(bool equip)
        {
            if (_hasCoat != equip)
            {
                _temperatureModification = equip ? _temperatureModification + 1 : _temperatureModification - 1;
                _hasCoat = equip;
            }
        }

        private IEnumerator StartCheckingTemperature()
        {
            yield return new WaitForSeconds(secondsToCheckAgain);
            _currentSecondsLapsed++;
            if (_currentSecondsLapsed % 5 == 0)
            {
                CheckTemperature();
                if (Heated || OverHeated) _playerMediator.Dehydrate(1);
            }

            if (_currentSecondsLapsed % 10 == 0)
            {
                if (OverHeated) _playerMediator.LoseLife(1);
                if (Frozen) _playerMediator.LoseLife(5);
            }
            
            if (_currentSecondsLapsed % 30 == 0)
            {
                if (Heated) _playerMediator.LoseLife(1);
            }
            
            
            
            
            StartCoroutine(StartCheckingTemperature());
        }

        public void CheckTemperature()
        {
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
            if (Temperature != _lastTemperature)
            {
                ChangeTemperatureLevel();
                _lastTemperature = Temperature;
            }
            Debug.Log($"MapTemperature:{mapTemperature}. CorporalTemperature: {Temperature}");
        }

        private void ChangeTemperatureLevel()
        {
            _onTemperatureChanged?.Invoke();
            switch (Temperature)
            {
                case <= 35:
                    _playerMediator.LimitStamina(19);
                    _playerMediator.StopLifeRegen(false);
                    _onTemperatureChanged = () =>
                    {
                        _playerMediator.StopLifeRegen(true);
                        _playerMediator.LimitStamina(100);
                    };
                    break;
                case 36:
                    _playerMediator.LimitStamina(19);
                    _playerMediator.StopLifeRegen(false);
                    _onTemperatureChanged = () =>
                    {
                        _playerMediator.StopLifeRegen(true);
                        _playerMediator.LimitStamina(100);
                    };
                    break;
                case 37:
                    
                    break;
                case 38:
                    _playerMediator.LimitStamina(60);
                    _playerMediator.StopLifeRegen(false);
                    _onTemperatureChanged = () =>
                    {
                        _playerMediator.StopLifeRegen(true);
                        _playerMediator.LimitStamina(100);
                    };
                    break;
                case >= 39:
                    _playerMediator.LimitStamina(60);
                    _playerMediator.StopLifeRegen(false);
                    _onTemperatureChanged = () =>
                    {
                        _playerMediator.StopLifeRegen(true);
                        _playerMediator.LimitStamina(100);
                    };
                    break;
            }
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
