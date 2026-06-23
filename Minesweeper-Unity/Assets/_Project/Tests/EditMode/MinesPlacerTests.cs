using System;
using NUnit.Framework;
using Minesweeper.Runtime.Gameplay;

namespace Minesweeper.Tests.EditMode
{
    public class MinesPlacerTests
    {
        private GridData _grid;
        private MinesPlacer _placer;

        [SetUp]
        public void SetUp()
        {
            _grid = TestsHelper.CreateGridData();
            _placer = new MinesPlacer(_grid, new Random());
        }

        [Test]
        public void PlaceMines_CorrectMineCount()
        {
            var cellData = _grid.GetCell(0, 0);
            _placer.PlaceMines(cellData);

            var count = 0;
            foreach (var cell in _grid.GetAllCells())
            {
                if (cell.IsMine) 
                    count++;
            }

            Assert.AreEqual(TestsHelper.MineCount, count);
        }
   

        [Test]
        public void PlaceMines_AdjacentMineCountIsCorrect()
        {
            var cellData = _grid.GetCell(0, 0);
            _placer.PlaceMines(cellData);

            foreach (var cell in _grid.GetAllCells())
            {
                if (cell.IsMine) continue;

                var expected = 0;
                foreach (var neighbour in _grid.GetNeighbours(cell.X, cell.Y))
                {
                    if (neighbour.IsMine) 
                        expected++;
                }

                Assert.AreEqual(expected, cell.AdjacentMineCount);
            }
        }
    }
}