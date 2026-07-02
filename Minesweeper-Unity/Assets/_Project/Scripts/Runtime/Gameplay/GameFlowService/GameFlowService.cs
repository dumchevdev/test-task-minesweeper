using System;
using Minesweeper.Runtime.UI;
using Minesweeper.Runtime.Infrastructure;

namespace Minesweeper.Runtime.Gameplay
{
    public class GameFlowService : IGameFlowService
    {
      private readonly IUIRouter _uiRouter;
        private readonly ITimerService _timerService;
        private readonly IInputsService _inputsService;
        private readonly ISessionsService _sessionsService;

        private GameFlowState _state = GameFlowState.MainMenu;
        private SessionStateType _sessionStateType;
        
        public event Action<SessionStateType> OnGameFinished;

        public GameFlowService(
            IUIRouter uiRouter,
            ITimerService timerService,
            IInputsService inputsService,
            ISessionsService sessionsService)
        {
            _uiRouter = uiRouter;
            _timerService = timerService;
            _inputsService = inputsService;
            _sessionsService = sessionsService;

            Subscribe();
        }
        
        public void StartGame() =>
            SetState(GameFlowState.MainMenu);

        public void Tick(float deltaTime) =>
            _timerService.Tick(deltaTime);

        public void StartNewSession()
        {
            _sessionsService.CreateNewSession();
            _timerService.Reset();
            SetState(GameFlowState.Playing);
        }

        public void PauseGame()
        {
            if (!_state.CanPause())
                return;

            _timerService.Stop();
            SetState(GameFlowState.Paused);
        }

        public void ContinueGame()
        {
            if (_state != GameFlowState.Paused)
                return;

            if (_sessionStateType.IsFinished())
            {
                ShowGameOver();
                return;
            }

            if (_sessionStateType.IsPlaying())
                _timerService.Start();

            SetState(GameFlowState.Playing);
        }

        public void ExitToMainMenu()
        {
            _sessionsService.ResetSession();
            _timerService.Reset();
            SetState(GameFlowState.MainMenu);
        }

        public void ShowGameOver()
        {
            _timerService.Stop();
            SetState(GameFlowState.GameOver);
        }
        
        private void Subscribe()
        {
            _inputsService.OnPausePressed += HandlePausePressed;
            _inputsService.OnRestartPressed += HandleRestartPressed;
            _sessionsService.OnSessionStateChanged += HandleSessionStateChanged;
        }

        private void Unsubscribe()
        {
            _inputsService.OnPausePressed -= HandlePausePressed;
            _inputsService.OnRestartPressed -= HandleRestartPressed;
            _sessionsService.OnSessionStateChanged -= HandleSessionStateChanged;
        }

        private void HandleRestartPressed() => StartNewSession();
        
        private void HandleSessionStateChanged(SessionStateType stateType)
        {
            _sessionStateType = stateType;
            
            if (stateType.IsPlaying())
            {
                _timerService.Start();
                return;
            }

            if (stateType.IsFinished())
            {
                ShowGameOver();
                OnGameFinished?.Invoke(stateType);
            }
        }

        private void HandlePausePressed()
        {
            switch (_state)
            {
                case GameFlowState.Paused:
                    ContinueGame();
                    break;
                default:
                    if (_state.CanPause())
                        PauseGame();
                    break;
            }
        }

        private void SetState(GameFlowState newState)
        {
            _state = newState;
            ApplyStateToUI(newState);
        }

        private void ApplyStateToUI(GameFlowState state)
        {
            switch (state)
            {
                case GameFlowState.MainMenu:
                    _uiRouter.ShowMainMenu();
                    break;
                case GameFlowState.Playing:
                    _uiRouter.ShowHud();
                    break;
                case GameFlowState.Paused:
                    _uiRouter.ShowPause();
                    break;
                case GameFlowState.GameOver:
                    _uiRouter.ShowGameOver();
                    break;
            }
        }

        public void Dispose() => Unsubscribe();
    }
}