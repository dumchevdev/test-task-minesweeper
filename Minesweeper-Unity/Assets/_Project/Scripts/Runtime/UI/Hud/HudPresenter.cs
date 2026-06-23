using System;
using VContainer.Unity;
using UnityEngine.EventSystems;
using Minesweeper.Runtime.Gameplay;
using Minesweeper.Runtime.Infrastructure;

namespace Minesweeper.Runtime.UI
{
    public class HudPresenter : IStartable, IDisposable
    {
        private readonly HudWindow _hudWindow;
        private readonly GridPresenter _gridPresenter;
        
        private readonly IGameFlowService _gameFlowService;
        private readonly ISessionsService _sessionsService;
        private readonly ITimerService _timerService;

        public HudPresenter(UIRoot uiRoot,
            ICellViewFactory cellViewFactory,
            IGameFlowService gameFlowService, 
            ISessionsService sessionsService, 
            ITimerService timerService)
        {
            _hudWindow = uiRoot.Hud;
            _gridPresenter = new GridPresenter(uiRoot.Hud.GridView, cellViewFactory);

            _gameFlowService = gameFlowService;
            _sessionsService = sessionsService;
            _timerService = timerService;
        }

        public void Start() => Subscribe();

        private void Subscribe()
        {
            _hudWindow.OnPauseClicked += HandlePauseClicked;
            _gridPresenter.OnCellClicked += HandleCellClicked;
            _gridPresenter.OnFlagCountChanged += HandleFlagCountChanged;
            _sessionsService.OnSessionStarted += HandleSessionStarted;
            _timerService.OnTick += HandleTimerUpdated;
        }
        
        private void Unsubscribe()
        {
            _hudWindow.OnPauseClicked -= HandlePauseClicked;
            _gridPresenter.OnCellClicked -= HandleCellClicked;
            _gridPresenter.OnFlagCountChanged -= HandleFlagCountChanged;
            _sessionsService.OnSessionStarted -= HandleSessionStarted;
            _timerService.OnTick -= HandleTimerUpdated;
        }

        private void HandlePauseClicked() 
            => _gameFlowService.PauseGame();
 
        private void HandleSessionStarted(GridData gridData) 
            => _gridPresenter.Bind(gridData);

        private void HandleFlagCountChanged(int flagCount) 
            => _hudWindow.FlagCounter.UpdateCount(flagCount);
        
        private void HandleTimerUpdated(float elapsedTime) 
            => _hudWindow.TimerView.UpdateTimer(elapsedTime);

        private void HandleCellClicked(CellData cellData, PointerEventData.InputButton button)
        {
            switch (button)
            {
                case PointerEventData.InputButton.Left:
                    _sessionsService.CurrentSession.OpenCell(cellData);
                    break;
                case PointerEventData.InputButton.Right:
                    _sessionsService.CurrentSession.ToggleFlag(cellData);
                    break;
            }
        }

        public void Dispose()
        {
            _gridPresenter.Dispose();
            Unsubscribe();
        }
    }
}