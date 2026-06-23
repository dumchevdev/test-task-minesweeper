using System;
using UnityEngine;
using VContainer.Unity;
using Minesweeper.Runtime.UI;
using Minesweeper.Runtime.Gameplay;

namespace Minesweeper.Runtime.Infrastructure
{
    public class BootstrapFlow : IStartable, ITickable, IDisposable
    {
        private readonly IConfigsProvider _configsProvider;
        private readonly IGameFlowService _gameFlowService;
        private readonly IUIRouter _uiRouter;

        public BootstrapFlow(
            IConfigsProvider configsProvider, 
            IGameFlowService gameFlowService, 
            IUIRouter uiRouter)
        {
            _configsProvider = configsProvider;
            _gameFlowService = gameFlowService;
            _uiRouter = uiRouter;
        }

        public void Start()
        {
            _configsProvider.Initialize();
            _uiRouter.ShowMainMenu();
        }

        public void Tick()
        {
            var deltaTime = Time.deltaTime;
            _gameFlowService.Tick(deltaTime);
        }

        public void Dispose()
        {
            _gameFlowService.Dispose();
        }
    }
}