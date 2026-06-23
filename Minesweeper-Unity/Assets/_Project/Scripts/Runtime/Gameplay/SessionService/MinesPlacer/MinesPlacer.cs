using System;
using System.Collections.Generic;

namespace Minesweeper.Runtime.Gameplay
{
    public class MinesPlacer
    {
        private readonly GridData _gridData;
        private readonly Random _random;
        private readonly List<CellData> _candidates;
        
        public MinesPlacer(GridData gridData, Random random)
        {
            _gridData = gridData;
            _random = random;
            _candidates = new List<CellData>(_gridData.Width * _gridData.Height);
        }
        
        public void PlaceMines() => Place(BuildCandidates());

        public void PlaceMines(CellData cellData) 
            => Place(BuildCandidates(cellData));

        private void Place(List<CellData> candidates)
        {
            ShuffleFirstN(candidates, _gridData.MineCount);
            
            for (var i = 0; i < _gridData.MineCount; i++)
                candidates[i].PlaceMine();
            
            CalculateAdjacentMines();
        }

        private List<CellData> BuildCandidates()
        {
            _candidates.Clear();
            foreach (var cell in _gridData.GetAllCells())
                _candidates.Add(cell);
            
            return _candidates;
        }

        private List<CellData> BuildCandidates(CellData cellData)
        {
            _candidates.Clear();
            foreach (var cell in _gridData.GetAllCells())
            {
                if (cell == cellData) continue;
                _candidates.Add(cell);
            }
                
            return _candidates;
        }
 
        private void ShuffleFirstN(List<CellData> list, int n)
        {
            for (var i = 0; i < n; i++)
            {
                var swapIndex = _random.Next(i, list.Count);
                (list[i], list[swapIndex]) = (list[swapIndex], list[i]);
            }
        }
        
        private void CalculateAdjacentMines()
        {
            foreach (var cell in _gridData.GetAllCells())
            {
                if (cell.IsMine)
                    continue;
 
                var count = 0;
                foreach (var neighbour in _gridData.GetNeighbours(cell.X, cell.Y))
                {
                    if (neighbour.IsMine)
                        count++;
                }
 
                cell.SetAdjacentMineCount(count);
            }
        }
    }
}