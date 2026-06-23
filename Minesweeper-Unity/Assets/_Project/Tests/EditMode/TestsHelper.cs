using Minesweeper.Runtime.Gameplay;

namespace Minesweeper.Tests.EditMode
{
    public static class TestsHelper
    {
        public const int MineCount = 10;

        private const int GridWidth = 9;
        private const int GridHeight = 9;

        public static GridData CreateGridData() 
            => new(GridWidth, GridHeight, MineCount);
    }
}