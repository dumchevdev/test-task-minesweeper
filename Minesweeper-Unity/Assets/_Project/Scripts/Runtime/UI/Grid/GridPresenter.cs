using System;
using System.Collections.Generic;
using Minesweeper.Runtime.Gameplay;
using UnityEngine.EventSystems;

namespace Minesweeper.Runtime.UI
{
    public class GridPresenter : IDisposable
    {
        private readonly GridView _gridView;
        private readonly ICellViewFactory _cellViewFactory;
        private readonly List<CellBinding> _bindings = new();
        
        private CellView[,] _cellViews;
        private int _remainingFlags;
        
        public event Action<CellData, PointerEventData.InputButton> OnCellClicked;
        public event Action<int> OnFlagCountChanged;

        public GridPresenter(GridView gridView, ICellViewFactory cellViewFactory)
        {
            _gridView = gridView;
            _cellViewFactory = cellViewFactory;
        }

        public void Bind(GridData gridData)
        {
            Unbind();

            _remainingFlags = gridData.MineCount;
            BuildGrid(gridData.Width, gridData.Height);

            for (var x = 0; x < gridData.Width; x++)
            for (var y = 0; y < gridData.Height; y++)
            {
                var cellView = _cellViews[x, y];
                var cellData = gridData.GetCell(x, y);
                BindCell(cellView, cellData);
            }

            OnFlagCountChanged?.Invoke(_remainingFlags);
        }
        
        private void BindCell(CellView view, CellData data)
        {
            var binding = new CellBinding(view, data);
            binding.OnClicked += HandleCellClicked;
            binding.OnFlagChanged += HandleFlagChanged;
            
            _bindings.Add(binding);
        }

        private void Unbind()
        {
            foreach (var binding in _bindings)
            {
                binding.OnClicked -= HandleCellClicked;
                binding.OnFlagChanged -= HandleFlagChanged;
                binding.Dispose();
            }
            
            _bindings.Clear();
        }
        
        private void BuildGrid(int width, int height)
        {
            if (HasSameSize(width, height)) return;
            Rebuild(width, height);
        }

        private bool HasSameSize(int width, int height)
            => _cellViews != null
               && _cellViews.GetLength(0) == width
               && _cellViews.GetLength(1) == height;

        private void Rebuild(int width, int height)
        {
            DestroyCells();
            
            _gridView.Setup(width, height);
            _cellViews = new CellView[width, height];
            
            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                var cellView = _cellViewFactory.Create(x, y);
                _cellViews[x, y] = cellView;
            }
        }

        private void DestroyCells()
        {
            if (_cellViews == null)
                return;
            
            var width = _cellViews.GetLength(0);
            var height = _cellViews.GetLength(1);
            
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                var cellView = _cellViews[x, y];
                UnityEngine.Object.Destroy(cellView.gameObject);
            }

            _cellViews = null;
        }
        
        private void HandleFlagChanged(bool isFlagged)
        {
            _remainingFlags += isFlagged ? -1 : 1;
            OnFlagCountChanged?.Invoke(_remainingFlags);
        }

        private void HandleCellClicked(CellData cellData, PointerEventData.InputButton button)
            => OnCellClicked?.Invoke(cellData, button);

        public void Dispose()
        {
            Unbind();
            DestroyCells();
        }
    }
}