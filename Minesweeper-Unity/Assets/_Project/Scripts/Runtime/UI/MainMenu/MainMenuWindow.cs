using System;
using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper.Runtime.UI
{
    public class MainMenuWindow : SimpleWindow
    {
        [SerializeField] private Button startButton;
        
        public event Action OnStartClicked;

        private void Awake()
        {
            startButton.onClick.AddListener(HandleStartClicked);    
        }
        
        private void HandleStartClicked() 
            => OnStartClicked?.Invoke();

        private void OnDestroy()
        {
            startButton.onClick.RemoveAllListeners();
        }
    }
}