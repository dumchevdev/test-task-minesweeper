using System;
using VContainer.Unity;
using Minesweeper.Runtime.Gameplay;

namespace Minesweeper.Runtime.UI
{
    public class GameOverPresenter : IStartable, IDisposable
    {
        private readonly GameOverWindow _gameOverWindow;
        private readonly IGameFlowService _gameFlowService;
        
        public GameOverPresenter(UIRoot uiRoot, IGameFlowService gameFlowService)
        {
            _gameOverWindow = uiRoot.GameOverWindow;
            _gameFlowService = gameFlowService;
        }

        public void Start()
        {
            _gameOverWindow.OnRestartClicked += HandleRestartClicked;
            _gameOverWindow.OnExitClicked += HandleExitClicked;
            _gameFlowService.OnGameFinished += HandleGameStateChanged;
        }

        private void HandleExitClicked() => _gameFlowService.ExitToMainMenu();
        private void HandleRestartClicked() => _gameFlowService.StartNewSession();

        private void HandleGameStateChanged(SessionStateType stateType) => 
            _gameOverWindow.SetResultLabel(stateType);

        public void Dispose()
        {
            _gameOverWindow.OnRestartClicked -= HandleRestartClicked;
            _gameOverWindow.OnExitClicked -= HandleExitClicked;
            _gameFlowService.OnGameFinished -= HandleGameStateChanged;
        }
    }
}