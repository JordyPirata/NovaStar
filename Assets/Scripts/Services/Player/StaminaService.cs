using System.Collections;
using Services.Interfaces;
using System;
using UnityEngine;
using System.Threading.Tasks;


namespace Services.Player
{
    public class StaminaService : MonoBehaviour, IStaminaService
    {
        private object lockObject = new();
        public bool Increase { get; set; }
        public int Stamina { get; private set; }
        public Action OnStatChanged { get; set; }
        public Action<bool> OnTiredChanged { get; set; }
        private Coroutine invokeOnTiredChangedCoroutine;
        private bool _isTired, _isStimulated;
        private int _maxStamina = 100;

        public bool IsTired
        {
            get => _isTired;
            set
            {
                if (_isTired != value)
                {
                    _isTired = value;
                    if (invokeOnTiredChangedCoroutine != null) return;
                    StartCoroutine(InvokeOnTiredChanged(value));
                }
            }
        }

        private IEnumerator InvokeOnTiredChanged(bool value)
        {
            OnTiredChanged?.Invoke(value);
            yield return new WaitForSeconds(5);
        }

        public void StartService()
        {
            StartCoroutine(ContinuousChange());
            Increase = true;
        }

        public void StopService()
        {
            StopCoroutine(ContinuousChange());
        }

        private void CheckIsTired()
        {
            if (Stamina < 20) IsTired = true;
            else IsTired = false;
        }

        public void DecreaseStat(int amount)
        {
            OnStatChanged.Invoke();
            if (Stamina - amount <= 0)
            {
                Stamina = 0;
                IsTired = true;
                return;
            }

            Stamina -= amount;
            if (Stamina == 0) IsTired = true;
        }

        public void Stimulate()
        {
            StopCoroutine(StimulateCoroutine());
            StartCoroutine(StimulateCoroutine());
        }

        public void SetMaxStamina(int cant)
        {
            _maxStamina = cant;
        }

        private IEnumerator StimulateCoroutine()
        {
            IncreaseStat(100);
            _isStimulated = true;
            yield return new WaitForSeconds(60);
            _isStimulated = false;
        }
        
        public void IncreaseStat(int amount)
        {
            OnStatChanged.Invoke();
            if (Stamina + amount >= _maxStamina)
            {
                Stamina = _maxStamina;
                return;
            }

            Stamina += amount;
            CheckIsTired();
        }

        /// <summary>
        /// Increase or decrease stamina over time
        /// </summary>
        /// <param name="increase"> If true, stamina will increase over time, otherwise it will decrease</param> 
        protected IEnumerator ContinuousChange()
        {
            while (true)
            {
                if (Increase || _isStimulated)
                {
                    yield return new WaitForSeconds(.20f);
                    IncreaseStat(2);
                }

                if (!Increase && !_isStimulated)
                {
                    yield return new WaitForSeconds(.1f);
                    DecreaseStat(2);
                }
            }
        }
    }
}