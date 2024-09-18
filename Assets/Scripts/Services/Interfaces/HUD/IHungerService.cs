using System;
using UnityEngine.Events;
namespace Services.Interfaces
{
    interface IHungerService
    {
        Action OnStatChanged { get; set; }
        int Hunger { get; set; }
        void StartService();
        void StopService();
        void IncreaseStat(int amount);
        void DecreaseStat(int amount);
    }
}
