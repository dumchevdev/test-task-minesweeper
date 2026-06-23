using System.Collections.Generic;

namespace Minesweeper.Runtime.Gameplay
{
    public class FloodFiller
    {
        private readonly GridData _grid;
 
        public FloodFiller(GridData grid)
        {
            _grid = grid;
        }
 
        public void FillFrom(CellData cellData)
        {
            var stack = new Stack<CellData>();
            var visited = new HashSet<CellData>();
 
            stack.Push(cellData);
            visited.Add(cellData);
 
            while (stack.Count > 0)
            {
                var cell = stack.Pop();
                if (!cell.IsClosed) continue;
 
                if (cell.IsFlagged)
                    cell.RemoveFlag();
                
                cell.Open();
 
                if (cell.AdjacentMineCount > 0)
                    continue;
 
                foreach (var neighbour in _grid.GetNeighbours(cell.X, cell.Y))
                {
                    if (!neighbour.IsMine && neighbour.IsClosed && visited.Add(neighbour))
                        stack.Push(neighbour);
                }
            }
        }
    }
}