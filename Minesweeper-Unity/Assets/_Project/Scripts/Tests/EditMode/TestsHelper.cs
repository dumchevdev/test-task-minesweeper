using System;
using Minesweeper.Runtime.Gameplay;
using Minesweeper.Runtime.Infrastructure;

namespace Minesweeper.Tests.EditMode
{
    public static class TestsHelper
    {
        public const int MineCount = 10;

        private const int RandomSeed = 42;
        private const int GridWidth = 9;
        private const int GridHeight = 9;

        public static GridData CreateGridData() => 
            new(GridWidth, GridHeight, MineCount);

        public static IRandomProvider CreateRandomProvider() => 
            new RandomProvider(new Random(RandomSeed));
    }
}