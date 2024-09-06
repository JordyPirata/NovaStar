using UnityEngine;
using System.Collections;

public abstract class StatService : MonoBehaviour
{
    public int Stat { get; set; }

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
            if (Stat < 100)
            {
                Stat += 1;
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