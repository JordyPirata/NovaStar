using System.Collections;
using Services.Interfaces;
using System;
using UnityEngine;

namespace Services.Player
{
public class StaminaService : StatService, IStaminaService
{
    public int Stamina { get => Stat; private set => Stat = value; }
    public Action OnTiredChanged { get; set; } 
    private bool _isTired;
    public bool IsTired
    {
        get => _isTired;
        set
        {
            if (_isTired != value)
            {
                _isTired = value;
                OnTiredChanged?.Invoke();
            }
        }
    }

    public override void DecreaseStat(int amount)
    {
        OnStatChanged.Invoke();
        if (Stamina - amount < 0) 
        {
            Stamina = 0;
            return;
        }
        Stamina -= amount;
        CheckIsTired();
    }

    private void CheckIsTired()
    {
        if (Stamina < 20) IsTired = true;
        else IsTired = false;
    }

    public override void IncreaseStat(int amount)
    {
        OnStatChanged.Invoke();
        if (Stamina + amount > 100) 
        {
            Stamina = 100;
            return;
        }
        Stamina += amount;
        CheckIsTired();
    }


    protected override IEnumerator NaturalRecovery()
    {
        Stamina = 0;
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Stamina <= 100)
            {
                IncreaseStat(2);
            }
        }
    }
}
}