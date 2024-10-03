using System.Collections;
using Services.Interfaces;
using System;
using UnityEngine;

namespace Services.Player
{
public class StaminaService : MonoBehaviour, IStaminaService
{
    public bool Increase { get; set; }
    public int Stamina { get; private set; }
    public Action OnStatChanged { get; set; }
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
            return;
        }
        Stamina -= amount;
        CheckIsTired();
    }
    
    public void IncreaseStat(int amount)
    {
        OnStatChanged.Invoke();
        if (Stamina + amount >= 100) 
        {
            Stamina = 100;
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
            if (Increase)
            {
                yield return new WaitForSeconds(.20f);
                IncreaseStat(1);
            }
            if (!Increase)
            {
                yield return new WaitForSeconds(.5f);
                DecreaseStat(1);
            }
        }
    }        
}
}