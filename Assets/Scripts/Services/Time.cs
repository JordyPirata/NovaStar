using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services.Utility
{
    public class Time
    {
        private float _seconds;
        private int _minutes, _horas;
        private readonly List<TimeService.DayLights> _dayLightsList;
        public Action<TimeService.DayLights> onChangeDayLight;
        private TimeService.DayLights _currentDayLight;


        public Time(int hora, int minuto, List<TimeService.DayLights> dayLightsList)
        {
            _horas = hora;
            _minutes = minuto;
            _dayLightsList = dayLightsList;
        }
        
        public void AddTime(float time)
        {
            _seconds += time;
            
            _minutes += (int)Math.Floor(_seconds / 60);
            _seconds %= 60;


            _horas += _minutes / 60;
            _minutes %= 60;

            if (_horas >= 24)
            {
                _horas -= 24;
            }
            
            foreach (var dayLight in _dayLightsList.Where(lights => lights.hour == _horas))
            {
                if (_currentDayLight != dayLight)
                {
                    _currentDayLight = dayLight;
                    onChangeDayLight?.Invoke(dayLight);   
                }
            }

            Debug.Log($"Son las {_horas}:{_minutes}:{_seconds}");
        }

        public string GetTime()
        {
            if (_minutes<10)
            {
                return $"{_horas}:0{_minutes}";
            }
            return $"{_horas}:{_minutes}";
        }
    }
}