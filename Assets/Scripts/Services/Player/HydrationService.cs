using Services.Interfaces;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;
public class HydrationService : StatService, IThirstService
{
    public int Hydration { get => Stat; set => Stat = value; }

    protected override IEnumerator NaturalRecovery()
    {
        Hydration = 100;
        while (true)
        {
            yield return new WaitForSeconds(5);
            if (Hydration <= 100 && Hydration > 0)
            {
                Hydration -= 1;
                OnStatChanged.Invoke();
            }
        }
    }
}
