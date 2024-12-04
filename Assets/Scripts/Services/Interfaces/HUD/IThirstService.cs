using System;
using UnityEngine.Events;
namespace Services.Interfaces
    {
    interface IThirstService
    {
        Action OnStatChanged { get; set; }
        int Hydration { get; set; }
        void StartService();
        void StopService();
        void IncreaseStat(int amount);
        void DecreaseStat(int amount);
        bool DrinkSomeWater();
    }   
}