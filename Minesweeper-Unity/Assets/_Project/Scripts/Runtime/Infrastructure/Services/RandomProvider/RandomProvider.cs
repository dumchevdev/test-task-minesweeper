using System;

namespace Minesweeper.Runtime.Infrastructure
{
    public class RandomProvider : IRandomProvider
    {
        private readonly Random _random;

        public RandomProvider(Random random)
        {
            _random = random;
        }

        public int Next(int minInclusive, int maxExclusive) => 
            _random.Next(minInclusive, maxExclusive);
    }
}