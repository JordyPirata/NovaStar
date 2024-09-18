using UnityEngine;
using System.Collections;
using System;

public abstract class StatService : MonoBehaviour
{
    public Action  OnStatChanged { get; set; } = new Action(() => { });
    public int Stat { get; set; }

    public void StartService()
    {
        StartCoroutine(NaturalRecovery());
    }

    public void StopService()
    {
        StopCoroutine(NaturalRecovery());
    }

    private IEnumerator NaturalRecovery()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Stat < 100)
            {
                Stat += 1;
                OnStatChanged.Invoke();
            }
        }
    }

    public void DecreaseStat(int amount)
    {
        Stat -= amount;
    }

    public void IncreaseStat(int amount)
    {
        Stat += amount;
    }
}