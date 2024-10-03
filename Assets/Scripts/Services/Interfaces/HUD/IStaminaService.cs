using System;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Services.Interfaces 
{
    public interface IStaminaService
    {
        bool Increase { get; set; }
        Action OnStatChanged { get; set; }
        Action OnTiredChanged { get; set; } 
        int Stamina { get; }
        bool IsTired { get; }
        void StartService();
        void StopService();
        void IncreaseStat(int amount);
        void DecreaseStat(int amount);
    }
}