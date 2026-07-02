using NUnit.Framework;
using Minesweeper.Runtime.Gameplay;

namespace Minesweeper.Tests.EditMode
{
    public class FloodFillerTests
    {
        private GridData _grid;
        private FloodFiller _filler;

        [SetUp]
        public void SetUp()
        {
            _grid = TestsHelper.CreateGridData();
            _filler = new FloodFiller(_grid);
        }

        [Test]
        public void FillFrom_OpensConnectedEmptyCells()
        {
            // All cells are empty - all should be opened
            var cellData = _grid.GetCell(0, 0);
            _filler.OpenFrom(cellData);

            foreach (var cell in _grid.GetAllCells())
                Assert.IsTrue(cell.IsOpen);
        }

        [Test]
        public void FillFrom_StopsAtNumberedCells()
        {
            // Place a mine in the corner — adjacent cells get AdjacentMineCount > 0
            _grid.GetCell(4, 4).PlaceMine();
            
            foreach (var cell in _grid.GetAllCells())
            {
                if (cell.IsMine) continue;
                
                var count = 0;
                foreach (var n in _grid.GetNeighbours(cell.X, cell.Y))
                {
                    if (n.IsMine)
                        count++;
                }
                
                cell.SetAdjacentMineCount(count);
            }

            var cellData = _grid.GetCell(0, 0);
            _filler.OpenFrom(cellData);

            // Cell adjacent to mine should open but not propagate further
            Assert.IsTrue(_grid.GetCell(3, 3).IsOpen);
            // Mine should not be revealed
            Assert.IsFalse(_grid.GetCell(4, 4).IsOpen);
        }

        [Test]
        public void FillFrom_RemovesFlagOnReveal()
        {
            var cell = _grid.GetCell(2, 2);
            cell.ToggleFlag();
            Assert.IsTrue(cell.IsFlagged);

            var cellData = _grid.GetCell(0, 0);
            _filler.OpenFrom(cellData);

            // Flag should be removed and cell opened
            Assert.IsFalse(cell.IsFlagged);
            Assert.IsTrue(cell.IsOpen);
        }
    }
}