using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper.Runtime.UI
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup layoutGroup;
        [SerializeField] private RectTransform viewport;
        
        public Transform Root => layoutGroup.transform;
        
        private void Awake()
        {
            layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        }

        public void Setup(int width, int height)
        {
            layoutGroup.constraintCount = width;
            UpdateScale(width, height);
        }

        private void UpdateScale(int width, int height)
        {
            var cellSize = layoutGroup.cellSize;
            var spacing = layoutGroup.spacing;
            var availableSize = viewport.rect.size;

            var calculatedScale = GridScaleCalculator.Calculate(width, height, 
                cellSize, spacing, availableSize);
            transform.localScale = Vector3.one * calculatedScale;
        }
    }
}