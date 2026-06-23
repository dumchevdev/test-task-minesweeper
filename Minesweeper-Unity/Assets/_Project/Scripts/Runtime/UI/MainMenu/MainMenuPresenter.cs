using System;
using VContainer.Unity;
using Minesweeper.Runtime.Gameplay;

namespace Minesweeper.Runtime.UI
{
    public class MainMenuPresenter : IStartable, IDisposable
    {
        private readonly MainMenuWindow _view;
        private readonly IGameFlowService _gameFlowService;
        
        public MainMenuPresenter(UIRoot uiRoot, IGameFlowService gameFlowService)
        {
            _view = uiRoot.MainMenu;
            _gameFlowService = gameFlowService;
        }

        public void Start()
        {
            _view.OnStartClicked += HandleStartClicked;
        }
        
        private void HandleStartClicked() 
            => _gameFlowService.StartNewGame();

        public void Dispose()
        {
            _view.OnStartClicked -= HandleStartClicked;
        }
    }
}