using System;
using System.Collections;
using Services.Interfaces;
using UnityEngine;

namespace Services.Player
{
public class HungerService : MonoBehaviour, IHungerService
{
    public int Hunger {get; set;}
    public Action OnStatChanged { get; set; }

    public void DecreaseStat(int amount)
    {
        Hunger -= amount;
        OnStatChanged.Invoke();
    }

    public void IncreaseStat(int amount)
    {
        Hunger += amount;
        OnStatChanged.Invoke();
    }

    public void StartService()
    {
        StartCoroutine(NaturalRecovery());
    }

    public void StopService()
    {
        StopCoroutine(NaturalRecovery());
    }

    protected IEnumerator NaturalRecovery()
    {
        Hunger = 100;
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