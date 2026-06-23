using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Minesweeper.Runtime.Gameplay;

namespace Minesweeper.Runtime.UI
{
    public class GameOverWindow : SimpleWindow
    {
        [Header("View")]
        [SerializeField] private TMP_Text resultLabel;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;
        
        [Header("Colors")]
        [SerializeField] private Color wonColor;
        [SerializeField] private Color lostColor;
        
        public event Action OnRestartClicked;
        public event Action OnExitClicked;

        private void Awake()
        {
            restartButton.onClick.AddListener(HandleRestartClicked);
            exitButton.onClick.AddListener(HandleExitClicked);
        }
        
        public void SetResultLabel(SessionStateType stateType)
        {
            var isWon = stateType == SessionStateType.Finished_Won;
            resultLabel.text = isWon ? "You Win!" : "You Lose!";
            resultLabel.color = isWon ? wonColor : lostColor;
        }
        
        private void HandleRestartClicked() => OnRestartClicked?.Invoke();
        private void HandleExitClicked() => OnExitClicked?.Invoke();

        private void OnDestroy()
        {
            restartButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
        }
    }
}