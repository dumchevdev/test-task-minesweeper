using System;
using VContainer.Unity;
using Minesweeper.Runtime.Gameplay;

namespace Minesweeper.Runtime.UI
{
    public class GameOverPresenter : IStartable, IDisposable
    {
        private readonly GameOverWindow _gameOverWindow;
        private readonly ISessionsService _sessionsService;
        private readonly IGameFlowService _gameFlowService;
        
        public GameOverPresenter(
            UIRoot uiRoot, 
            ISessionsService sessionsService, 
            IGameFlowService gameFlowService)
        {
            _gameOverWindow = uiRoot.GameOverWindow;
            _sessionsService = sessionsService;
            _gameFlowService = gameFlowService;
        }

        public void Start()
        {
            _gameOverWindow.OnRestartClicked += HandleRestartClicked;
            _gameOverWindow.OnExitClicked += HandleExitClicked;
            _sessionsService.OnSessionStateChanged += HandleSessionStateChanged;
        }

        private void HandleExitClicked() => _gameFlowService.ExitToMainMenu();
        private void HandleRestartClicked() => _gameFlowService.StartNewGame();

        private void HandleSessionStateChanged(SessionStateType stateType)
        {
            var isFinished = stateType == SessionStateType.Finished_Lost ||
                             stateType == SessionStateType.Finished_Won;
            
            if (!isFinished) return;
            
            _gameOverWindow.SetResultLabel(stateType);
            _gameFlowService.ShowGameOver();
        }

        public void Dispose()
        {
            _gameOverWindow.OnRestartClicked -= HandleRestartClicked;
            _gameOverWindow.OnExitClicked -= HandleExitClicked;
            _sessionsService.OnSessionStateChanged -= HandleSessionStateChanged;
        }
    }
}