using System.Collections.Generic;

namespace Minesweeper.Runtime.Gameplay
{
    public class FloodFiller
    {
        private readonly GridData _grid;
        private readonly Stack<CellData> _cachedStack = new();
        private readonly HashSet<CellData> _cachedVisited = new();
 
        public FloodFiller(GridData grid)
        {
            _grid = grid;
        }
 
        public int OpenFrom(CellData cellData)
        {
            _cachedStack.Clear();
            _cachedVisited.Clear();

            var openedCount = 0;

            _cachedStack.Push(cellData);
            _cachedVisited.Add(cellData);
 
            while (_cachedStack.Count > 0)
            {
                var cell = _cachedStack.Pop();
                if (!cell.IsClosed) continue;
 
                cell.OpenAndClearFlag();
                openedCount++;

                if (cell.AdjacentMineCount > 0)
                    continue;
 
                foreach (var neighbour in _grid.GetNeighbours(cell.X, cell.Y))
                {
                    if (!neighbour.IsMine && neighbour.IsClosed && _cachedVisited.Add(neighbour))
                        _cachedStack.Push(neighbour);
                }
            }

            return openedCount;
        }
    }
}