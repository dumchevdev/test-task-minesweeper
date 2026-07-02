using UnityEngine;

namespace Minesweeper.Runtime.Configs
{
    [CreateAssetMenu(fileName = "CellViewConfig", menuName = "Minesweeper/Cell View Config")]
    public class CellViewConfig : ScriptableObject
    {
        [Header("SPRITES")]
        [SerializeField] private Sprite openedCellSprite;
        [SerializeField] private Sprite closedCellSprite;
        [SerializeField] private Sprite flaggedCellSprite;
        [SerializeField] private Sprite flaggedMineCellSprite;
        [SerializeField] private Sprite minedCellSprite;
        [SerializeField] private Sprite triggeredMineCellSprite;

        [Header("COLORS")]
        [SerializeField] private Color[] numbersColor;

        public Sprite OpenedCellSprite => openedCellSprite;
        public Sprite ClosedCellSprite => closedCellSprite;
        public Sprite FlaggedCellSprite => flaggedCellSprite;
        public Sprite FlaggedMineCellSprite => flaggedMineCellSprite;
        public Sprite MinedCellSprite => minedCellSprite;
        public Sprite TriggeredMineCellSprite => triggeredMineCellSprite;
        public Color[] NumbersColor => numbersColor;
    }
}