using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Minesweeper.Runtime.Gameplay;
using Minesweeper.Runtime.Configs;

namespace Minesweeper.Runtime.UI
{
    public class CellView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image background;
        [SerializeField] private TMP_Text label;

        private CellViewConfig _viewConfig;

        public event Action<PointerEventData.InputButton> OnClicked;

        public void Initialize(CellViewConfig viewConfig)
        {
            _viewConfig = viewConfig;
            RenderClosed();
        }

        public void Render(CellData cellData)
        {
            if (cellData.IsOpen)
            {
                RenderOpen(cellData);
                return;
            }

            if (cellData.IsFlagged)
            {
                RenderFlagged();
                return;
            }

            RenderClosed();
        }

        public void OnPointerClick(PointerEventData eventData) => 
            OnClicked?.Invoke(eventData.button);

        private void RenderClosed()
        {
            background.sprite = _viewConfig.ClosedCellSprite;
            background.color = Color.white;
            label.text = string.Empty;
        }

        private void RenderFlagged()
        {
            background.sprite = _viewConfig.FlaggedCellSprite;
            background.color = Color.white;
            label.text = string.Empty;
        }

        private void RenderOpen(CellData cellData)
        {
            label.text = string.Empty;
            background.color = Color.white;

            if (cellData.IsMine)
            {
                RenderMine(cellData);
                return;
            }

            background.sprite = _viewConfig.OpenedCellSprite;

            if (cellData.AdjacentMineCount > 0)
            {
                label.text = cellData.AdjacentMineCount.ToString();
                label.color = cellData.AdjacentMineCount < _viewConfig.NumbersColor.Length
                    ? _viewConfig.NumbersColor[cellData.AdjacentMineCount]
                    : Color.white;
            }
        }

        private void RenderMine(CellData cellData)
        {
            if (cellData.IsExploded)
            {
                background.sprite = _viewConfig.TriggeredMineCellSprite;
                return;
            }

            if (cellData.IsFlagged)
            {
                background.sprite = _viewConfig.FlaggedMineCellSprite;
                return;
            }

            background.sprite = _viewConfig.MinedCellSprite;
        }
    }
}