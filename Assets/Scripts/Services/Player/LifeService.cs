using System.Collections;
using Services.Interfaces;
using UnityEngine;
using System;
public class LifeService : MonoBehaviour, ILifeService
{
    public int Life { get; set; }
    public Action OnStatChanged { get; set; }

    public void DecreaseStat(int amount)
    {
        Life -= amount;
        OnStatChanged.Invoke();
    }

    public void IncreaseStat(int amount)
    {
        Life += amount;
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
        Life = 100;
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Life <= 100 && Life > 0)
            {
                Life += 1;
                OnStatChanged.Invoke();
            }
        }
    }

}
