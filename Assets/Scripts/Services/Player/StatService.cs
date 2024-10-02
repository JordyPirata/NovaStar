using UnityEngine;
using System.Collections;
using System;

public class StatService : MonoBehaviour
{
    public Action OnStatChanged { get; set; } = new Action(() => { });
    
    protected int Stat { get; set; }

    public void StartService()
    {
        StartCoroutine(NaturalRecovery());
    }

    public void StopService()
    {
        StopCoroutine(NaturalRecovery());
    }

    protected virtual IEnumerator NaturalRecovery()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Stat <= 100)
            {
                IncreaseStat(1);
            }
        }
    }
    
    public virtual void DecreaseStat(int amount)
    {
        OnStatChanged.Invoke();
        if (Stat - amount <= 0) 
        {
            Stat = 0;
            return;
        }
        Stat -= amount;
    }

    public virtual void IncreaseStat(int amount)
    {
        OnStatChanged.Invoke();
        if (Stat + amount >= 100) 
        {
            Stat = 100;
            return;
        }
        Stat += amount;
    }
}