using System.Collections;
using Services.Interfaces;
using System;
using UnityEngine;

namespace Services.Player
{
public class StaminaService : StatService, IStaminaService
{
    public Action IsTen { get; set; } = new Action(() => { });

    public int Stamina { get => Stat; set => Stat = value; }
    
    protected override IEnumerator NaturalRecovery()
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
            if (Stamina == 10)
            {
                IsTen.Invoke();
            }
        }
    }
}
}