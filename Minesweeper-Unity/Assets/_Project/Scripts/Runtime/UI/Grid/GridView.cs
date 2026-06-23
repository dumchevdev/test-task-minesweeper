using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper.Runtime.UI
{
    public class GridView : MonoBehaviour
    {
        private const int MinCellCount = 9;
        private const int MaxCellCount = 1024;
        private const float MaxScale = 1f;
        private const float MinScale = 0.3f;
        private const float ScaleCurvePower = 0.4f;
        
        [SerializeField] private GridLayoutGroup layoutGroup;
       
        public Transform Root => layoutGroup.transform;

        public void Setup(int width, int height)
        {
            layoutGroup.constraintCount = width;
            UpdateScale(width, height);
        }
        
        private void UpdateScale(int width, int height)
        {
            var cellCount = width * height;
            var scaleProgress = Mathf.InverseLerp(MinCellCount, MaxCellCount, cellCount);
            scaleProgress = Mathf.Pow(scaleProgress, ScaleCurvePower);

            var scale = Mathf.Lerp(MaxScale, MinScale, scaleProgress);
            transform.localScale = Vector3.one * scale;
        }
    }
}