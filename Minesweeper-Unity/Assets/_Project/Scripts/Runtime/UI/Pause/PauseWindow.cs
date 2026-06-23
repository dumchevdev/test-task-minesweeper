using System;
using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper.Runtime.UI
{
    public class PauseWindow : SimpleWindow
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;
        
        public event Action OnContinueClicked;
        public event Action OnRestartClicked;
        public event Action OnExitClicked;
        
        private void Awake()
        {
            continueButton.onClick.AddListener(HandleContinueClicked);
            restartButton.onClick.AddListener(HandleRestartClicked);
            exitButton.onClick.AddListener(HandleExitClicked);
        }

        private void HandleContinueClicked()
        {
            OnContinueClicked?.Invoke();
        }

        private void HandleRestartClicked()
        {
            OnRestartClicked?.Invoke();
        }

        private void HandleExitClicked()
        {
            OnExitClicked?.Invoke();
        }

        private void OnDestroy()
        {
            continueButton.onClick.RemoveAllListeners();
            restartButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
        }
    }
}