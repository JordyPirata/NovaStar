using Services.Interfaces;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;
public class HydrationService : MonoBehaviour, IThirstService
{
    public int Hydration { get; set; }
    public Action OnStatChanged { get; set; }

    public void DecreaseStat(int amount)
    {
        Hydration -= amount;
        OnStatChanged.Invoke();
    }

    public void IncreaseStat(int amount)
    {
        Hydration += amount;
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
