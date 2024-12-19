using System;
using Unity.Mathematics;

namespace Models
{
    [Serializable]
    public class PlayerStatsModel
    {
        public float3 playerPosition;
        public int playerLife;
        public int playerThirsty;
        public int playerHunger;
    }
}