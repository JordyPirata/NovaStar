using System.Collections;
using Services.Interfaces;
using System;
using UnityEngine;

namespace Services.Player
{
public class StaminaService : MonoBehaviour, IStaminaService
{
    public int Stamina { get; private set; }
    public Action OnStatChanged { get; set; }
    public Action OnTiredChanged { get; set; }
    public bool IsTired
    {
        get => IsTired;
        set
        {
            if (IsTired != value)
            {
                IsTired = value;
                OnTiredChanged?.Invoke();
            }
        }
    }

    public void DecreaseStat(int amount)
    {
        OnStatChanged.Invoke();
        if (Stamina - amount < 0) 
        {
            Stamina = 0;
            return;
        }
        Stamina -= amount;
        if (amount < 20) IsTired = true;
    }

    public void IncreaseStat(int amount)
    {
        OnStatChanged.Invoke();
        if (Stamina + amount > 100) 
        {
            Stamina = 100;
            return;
        }
        Stamina += amount;
        if (amount > 20) IsTired = false;
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
        Stamina = 100;
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Stamina <= 100)
            {
                Stamina -= 10;
                OnStatChanged.Invoke();
            }
        }
    }
}
}