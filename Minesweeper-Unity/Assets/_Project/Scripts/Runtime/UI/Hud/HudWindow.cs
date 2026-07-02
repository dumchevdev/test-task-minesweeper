using System;
using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper.Runtime.UI
{
    public class HudWindow : SimpleWindow
    {
        [SerializeField] private GridView gridView;
        [SerializeField] private Button pauseButton;
        [SerializeField] private TimerView timerView;
        [SerializeField] private FlagCounterView flagCounterView;

        public GridView GridView => gridView;
        public TimerView TimerView => timerView;
        public FlagCounterView FlagCounter => flagCounterView;

        public event Action OnPauseClicked;

        private void Awake()
        {
            pauseButton.onClick.AddListener(HandlePauseClicked);
        }

        private void HandlePauseClicked() => OnPauseClicked?.Invoke();
        
        private void OnDestroy()
        {
            pauseButton.onClick.RemoveAllListeners();
        }
    }
}