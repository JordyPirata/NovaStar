using System.Collections;
using Services.Interfaces;
using UnityEngine;

namespace Services.Player
{
    public class LifeService : StatService, ILifeService
    {
        public int Life { get => Stat; set => Stat = value; }
        private bool _stimulated;
        private int _restingStimulatedTime;
        public void Stimulate()
        {
            _stimulated = true;
            _restingStimulatedTime = 60;
            if (_stimulated) StopCoroutine(StimulatedRecovery());
            StartCoroutine(StimulatedRecovery());
        }

        protected override IEnumerator NaturalRecovery()
        {
            Life = 99;
            while (true)
            {
                yield return new WaitForSeconds(1);
                if (Life < 100)
                {
                    Life += 1;
                    OnStatChanged.Invoke();
                }
            }
        }

        private IEnumerator StimulatedRecovery()
        {
            if (Life < 100)
            {
                Life += 1;
                _restingStimulatedTime--;
                if (_restingStimulatedTime <= 0) _stimulated = false;
                OnStatChanged.Invoke();
            }
            yield return new WaitForSeconds(1);
        }
    }
}
