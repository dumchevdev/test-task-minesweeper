using System;
using TMPro;
using UnityEngine;

namespace Minesweeper.Runtime.UI
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
 
        public void UpdateTimer(float elapsed)
        {
            var ts = TimeSpan.FromSeconds(elapsed);
            label.text = $"{ts.Minutes:00}:{ts.Seconds:00}";
        }
    }
}