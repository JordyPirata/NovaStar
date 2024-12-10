using System;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Services.Interfaces 
{
    public interface IStaminaService
    {
        bool Increase { get; set; }
        bool IsTired { get; }
        Action OnStatChanged { get; set; }
        Action<bool> OnTiredChanged { get; set; }
        int Stamina { get; }
        void StartService();
        void StopService();
        void IncreaseStat(int amount);
        void DecreaseStat(int amount);
        void Stimulate();
        void SetMaxStamina(int cant);
    }
}