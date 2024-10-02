using System.Collections;
using Services.Interfaces;
using UnityEngine;
using System;
public class LifeService : StatService, ILifeService
{
    public int Life { get => Stat; set => Stat = value; }

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

}
