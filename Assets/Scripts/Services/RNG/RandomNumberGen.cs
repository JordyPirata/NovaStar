using Services.Interfaces;
using System;

namespace Services.RNG
{
    public class RandomNumberGen : IRandomNumberGen
    {
        private Random _random;

        public RandomNumberGen()
        {
            _random = new Random();
        }

        public int Next(int min, int max)
        {
            return _random.Next(min, max);
        }
    }

    //TODO: Implement a better random number generator with a seed
}