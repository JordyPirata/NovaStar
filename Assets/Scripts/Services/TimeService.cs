using System;
using System.Collections;
using System.Collections.Generic;
using Services.Interfaces;
using UnityEngine;

namespace Services
{
    public class TimeService : MonoBehaviour, ITimeService
    {
        [Serializable]
        public class DayLights
        {
            public int hour;
            public float intensity;
            public int temperature;
        }
        private Utility.Time _currentTime;

        [SerializeField] private int beginningHour, beginningMinute, timeVelocity;
        [SerializeField] private Light mainLight;
        [SerializeField] private List<DayLights> dayLightsList;
        [SerializeField] private float dayLightChangeTime = 2f;
        [SerializeField] private float timeLoopInterval;
        private bool _contarElTiempo;

        private void Awake()
        {
            _currentTime = new Utility.Time(beginningHour, beginningMinute, dayLightsList);
            _currentTime.onChangeDayLight += OnChangeDayLight;
        }

        private void OnChangeDayLight(DayLights dayLight)
        {
            StartCoroutine(ChangeDayLight(dayLight));
        }

        private IEnumerator ChangeDayLight(DayLights dayLight)
        {
            var elapsedTime = 0f;
            var startingIntensity = mainLight.intensity;
            var startingTemperature = mainLight.colorTemperature;
            while (elapsedTime <= dayLightChangeTime)
            {
                mainLight.intensity = Mathf.Lerp(startingIntensity, dayLight.intensity, elapsedTime);
                mainLight.colorTemperature = Mathf.Lerp(startingTemperature, dayLight.temperature, elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        private void Update()
        {
        
        }

        
        
        private IEnumerator AddTime()
        {
            yield return new WaitForSeconds(timeLoopInterval);
            _currentTime.AddTime(timeLoopInterval * timeVelocity);
            StartCoroutine(AddTime());
        }

        public string GetTime()
        {
            return _currentTime.GetTime();
        }

        public void SpendTime(float minutesInBike)
        {
            _currentTime.AddTime(minutesInBike * 60);
        }

        public void StartRunningTime()
        {
            StartCoroutine(AddTime());
        }
    }
}