using System;
using UnityEngine;
using VContainer.Unity;
using Minesweeper.Runtime.Gameplay;

namespace Minesweeper.Runtime.Infrastructure
{
    public class BootstrapFlow : IStartable, ITickable, IDisposable
    {
        private readonly IConfigsProvider _configsProvider;
        private readonly IGameFlowService _gameFlowService;

        public BootstrapFlow(
            IConfigsProvider configsProvider, 
            IGameFlowService gameFlowService)
        {
            _configsProvider = configsProvider;
            _gameFlowService = gameFlowService;
        }

        public void Start()
        {
            _configsProvider.Initialize();
            _gameFlowService.StartGame();
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