using System;
using VContainer.Unity;
using Minesweeper.Runtime.Gameplay;

namespace Minesweeper.Runtime.UI
{
    public class PausePresenter : IStartable, IDisposable
    {
        private readonly PauseWindow _view;
        private readonly IGameFlowService _gameFlowService;
        
        public PausePresenter(UIRoot uiRoot, IGameFlowService gameFlowService)
        {
            _view = uiRoot.Pause;
            _gameFlowService = gameFlowService;
        }
        
        public void Start()
        {
            _view.OnContinueClicked += HandleContinueClicked;
            _view.OnRestartClicked += HandleRestartClicked;
            _view.OnExitClicked += HandleExitClicked;
        }

        private void HandleContinueClicked()
            => _gameFlowService.ContinueGame();

        private void HandleRestartClicked() 
            => _gameFlowService.StartNewGame();

        private void HandleExitClicked() 
            => _gameFlowService.ExitToMainMenu();

        public void Dispose()
        {
            _view.OnContinueClicked -= HandleContinueClicked;
            _view.OnRestartClicked -= HandleRestartClicked;
            _view.OnExitClicked -= HandleExitClicked;
        }
    }
}