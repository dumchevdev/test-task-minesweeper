using UnityEngine;

namespace Minesweeper.Runtime.Configs
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "Minesweeper/Grid Config")]
    public class GridConfig : ScriptableObject
    {
        [Header("SETTINGS")]
        [Range(4, 32), SerializeField] private int width = 9;
        [Range(4, 32), SerializeField] private int height = 9;
        [Min(0), SerializeField] private int minesCount = 10;

        public int Width => width;
        public int Height => height;
        public int MinesCount => minesCount;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            var max = width * height - 1;
            if (minesCount <= max)
                return;
            
            minesCount = max;
            Debug.LogWarning($"[GridConfig] Mine count clamped to {max} (max for {width}x{height} grid)");
        }
#endif
    }
}

