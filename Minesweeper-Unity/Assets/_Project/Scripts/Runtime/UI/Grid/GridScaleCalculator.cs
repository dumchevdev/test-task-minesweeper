using UnityEngine;

namespace Minesweeper.Runtime.UI
{
    public static class GridScaleCalculator
    {
        private const float MinScale = 0.3f;
        private const float MaxScale = 1f;
        
        public static float Calculate(int width, int height, 
            Vector2 cellSize, Vector2 spacing, Vector2 availableSize)
        {
            var contentWidth  = width  * cellSize.x + Mathf.Max(0, width  - 1) * spacing.x;
            var contentHeight = height * cellSize.y + Mathf.Max(0, height - 1) * spacing.y;

            if (contentWidth <= 0f || contentHeight <= 0f) 
                return MaxScale;

            var scaleX = availableSize.x / contentWidth;
            var scaleY = availableSize.y / contentHeight;

            var limitedScale = Mathf.Min(scaleX, scaleY);
            return Mathf.Clamp(limitedScale, MinScale, MaxScale);
        }
    }
}