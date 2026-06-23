using System;
using System.Collections.Generic;

namespace Minesweeper.Runtime.Gameplay
{
    public class GridData
    {
        private readonly CellData[,] _cells;
        private readonly (int dx, int dy)[] _neighbourOffsets =
        {
            (-1, -1), (0, -1), (1, -1),
            (-1,  0),          (1,  0),
            (-1,  1), (0,  1), (1,  1)
        };
 
        public int Width { get; }
        public int Height { get; }
        public int MineCount { get; }
 
        public GridData(int width, int height, int mineCount)
        {
            Width = width;
            Height = height;
            MineCount = mineCount;
            
            _cells = new CellData[width, height];
 
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
                _cells[x, y] = new CellData(x, y);
        }
 
        public CellData GetCell(int x, int y)
        {
            if (!IsInBounds(x, y))
                throw new ArgumentOutOfRangeException($"Cell ({x},{y}) is out of bounds");
 
            return _cells[x, y];
        }
 
        public IEnumerable<CellData> GetNeighbours(int x, int y)
        {
            foreach (var (dx, dy) in _neighbourOffsets)
            {
                var nx = x + dx;
                var ny = y + dy;
                if (IsInBounds(nx, ny))
                    yield return _cells[nx, ny];
            }
        }
 
        public IEnumerable<CellData> GetAllCells()
        {
            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
                yield return _cells[x, y];
        }
 
        private bool IsInBounds(int x, int y) =>
            x >= 0 && x < Width && y >= 0 && y < Height;
    }
}