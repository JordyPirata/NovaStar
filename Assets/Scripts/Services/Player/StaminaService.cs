using System.Collections;
using Services.Interfaces;
using UnityEngine;
namespace Services.Player
{
    public class StaminaService : StatService, IStaminaService
    {
        public int Stamina { get => Stat; set => Stat = value; }
    }
}