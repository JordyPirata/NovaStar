using System;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Services.Interfaces 
{
    public interface IStaminaService
    {
        Action OnStatChanged { get; set; }
        int Stamina { get; set; }
        void StartService();
        void StopService();
        void IncreaseStat(int amount);
        void DecreaseStat(int amount);
    }
}