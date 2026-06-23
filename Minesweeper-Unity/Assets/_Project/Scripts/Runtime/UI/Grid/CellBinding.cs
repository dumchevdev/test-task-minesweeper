using System;
using UnityEngine.EventSystems;
using Minesweeper.Runtime.Gameplay;

namespace Minesweeper.Runtime.UI
{
    public class CellBinding : IDisposable
    {
        private readonly CellView _view;
        private readonly CellData _data;

        public event Action<bool> OnFlagChanged;
        public event Action<CellData, PointerEventData.InputButton> OnClicked;

        public CellBinding(CellView view, CellData data)
        {
            _view = view;
            _data = data;

            Render();
            Subscribe();
        }

        private void Subscribe()
        {
            _data.OnChanged += Render;
            _data.OnFlaggedChanged += HandleFlagChanged;
            _view.OnClicked += HandleCellClicked;
        }
        
        private void Unsubscribe()
        {
            _data.OnChanged -= Render;
            _data.OnFlaggedChanged -= HandleFlagChanged;
            _view.OnClicked -= HandleCellClicked;
        }

        private void Render() => _view.Render(_data);
        
        private void HandleFlagChanged() 
            => OnFlagChanged?.Invoke(_data.IsFlagged);
        
        private void HandleCellClicked(PointerEventData.InputButton button) 
            => OnClicked?.Invoke(_data, button);

        public void Dispose() => Unsubscribe();
    }
}