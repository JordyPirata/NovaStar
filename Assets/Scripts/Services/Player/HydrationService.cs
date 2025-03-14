using Services.Interfaces;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;
public class HydrationService : StatService, IThirstService
{
    private const int bottleWaterHydrationRegen = 20;
    public int Hydration { get => Stat; set => Stat = value; }
    public bool DrinkSomeWater()
    {
        if (Hydration >= 100) return false;
        IncreaseStat(bottleWaterHydrationRegen);
        return true;
    }

    protected override IEnumerator NaturalRecovery()
    {
        Hydration = 99;
        while (true)
        {
            yield return new WaitForSeconds(5);
            if (Hydration < 100 && Hydration > 0)
            {
                Hydration -= 1;
                OnStatChanged.Invoke();
            }
        }
    }
}
