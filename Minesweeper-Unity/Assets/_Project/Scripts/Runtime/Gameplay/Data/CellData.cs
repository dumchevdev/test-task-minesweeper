using System;

namespace Minesweeper.Runtime.Gameplay
{
    public enum CellState
    {
        Closed,
        Open
    }

    [Flags]
    public enum CellAttributes
    {
        None = 0,
        Mine = 1 << 0,
        Flagged = 1 << 1,
        Exploded = 1 << 2
    }

    public class CellData
    {
        private CellState _state;
        private CellAttributes _attributes;
        
        public int X { get; }
        public int Y { get; }
        public int AdjacentMineCount { get; private set; }

        public bool IsOpen => _state == CellState.Open;
        public bool IsClosed => _state == CellState.Closed;
        
        public bool IsMine => (_attributes & CellAttributes.Mine) != 0;
        public bool IsFlagged => (_attributes & CellAttributes.Flagged) != 0;
        public bool IsExploded => (_attributes & CellAttributes.Exploded) != 0;

        public event Action OnChanged;
        public event Action OnFlaggedChanged;

        public CellData(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void PlaceMine() => _attributes |= CellAttributes.Mine;
        public void SetAdjacentMineCount(int count) => AdjacentMineCount = count;

        public void Open()
        {
            _state = CellState.Open;
            OnChanged?.Invoke();
        }
        
        public void OpenAndClearFlag()
        {
            var wasFlagged = IsFlagged;
            _attributes &= ~CellAttributes.Flagged;
            _state = CellState.Open;

            OnChanged?.Invoke();
            if (wasFlagged) OnFlaggedChanged?.Invoke();
        }

        public void Explode()
        {
            _attributes |= CellAttributes.Exploded;
            Open();
        }

        public void ToggleFlag()
        {
            _attributes ^= CellAttributes.Flagged;
            
            OnChanged?.Invoke();
            OnFlaggedChanged?.Invoke();
        }
    }
}