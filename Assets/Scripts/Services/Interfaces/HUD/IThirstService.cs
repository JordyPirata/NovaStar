using System;
using UnityEngine.Events;
namespace Services.Interfaces
    {
    interface IThirstService
    {
        Action OnStatChanged { get; set; }
        int Hydration { get; set; }
        void IncreaseStat(int amount);
        void DecreaseStat(int amount);
    }   
}