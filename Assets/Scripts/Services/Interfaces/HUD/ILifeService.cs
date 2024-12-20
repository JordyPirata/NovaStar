using System;
using UnityEngine.Events;
namespace Services.Interfaces
{
    public interface ILifeService
    {
        Action OnStatChanged { get; set; }
        int Life { get; set; }
        void IncreaseStat(int amount);
        void DecreaseStat(int amount);
        void Stimulate();
    }
}
