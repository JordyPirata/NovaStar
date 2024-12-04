using System;
using System.Collections;
using Services.Interfaces;
using UnityEngine;

namespace Services.Player
{
public class HungerService : StatService, IHungerService
{
    private const int foodHungerRegen = 25;
    public int Hunger { get => Stat; set => Stat = value; }
    public bool EatSomeFood()
    {
        if (Hunger >= 100) return false;
        IncreaseStat(foodHungerRegen);
        return true;
    }

    protected override IEnumerator NaturalRecovery()
    {
        Hunger = 99;
        while (true)
        {
            yield return new WaitForSeconds(5);
            if (Hunger <= 100 && Hunger > 0)
            {
                Hunger -= 1;
                OnStatChanged.Invoke();
            }
        }
    }
}
}