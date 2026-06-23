using Minesweeper.Runtime.UI;
using Minesweeper.Runtime.Infrastructure;

namespace Minesweeper.Runtime.Gameplay
{
    public class GameFlowService : IGameFlowService
    {
        private readonly ISessionsService _sessionsService;
        private readonly IUIRouter _uiRouter;
        private readonly ITimerService _timerService;
        private readonly IInputsService _inputsService;
        
        private bool _isPaused;
        
        public GameFlowService(
            IUIRouter uiRouter, 
            ISessionsService sessionsService, 
            ITimerService timerService, 
            IInputsService inputsService)
        {
            _uiRouter = uiRouter;
            _sessionsService = sessionsService;
            _timerService = timerService;
            _inputsService = inputsService;

            Subscribe();
        }

        public void StartNewGame()
        {
            _sessionsService.CreateNewSession();
            _timerService.Reset();
            _uiRouter.ShowHud();
        }

        public void PauseGame()
        {
            _timerService.Stop();
            _uiRouter.ShowPause();
        }

        public void ContinueGame()
        {
            var sessionType = _sessionsService.CurrentSession.StateType;
            if (CanStartTimer(sessionType)) 
                _timerService.Start();
            
            if (IsGameOver(sessionType)) _uiRouter.ShowGameOver();
            else _uiRouter.ShowHud();
        }

        public void ExitToMainMenu()
        {
            _sessionsService.ResetSession();
            _timerService.Reset();
            _uiRouter.ShowMainMenu();
        }

        public void ShowGameOver()
        {
            _timerService.Stop();
            _uiRouter.ShowGameOver();
        }

        public void Tick(float deltaTime)
        {
            _timerService.Tick(deltaTime);
        }
        
        private void Subscribe()
        {
            _inputsService.OnPausePressed += HandlePausePressed;
            _inputsService.OnRestartPressed += StartNewGame;
            _sessionsService.OnSessionStateChanged += HandleSessionStateChanged;
        }
        
        private void Unsubscribe()
        {
            _inputsService.OnPausePressed -= HandlePausePressed;
            _inputsService.OnRestartPressed -= StartNewGame;
            _sessionsService.OnSessionStateChanged -= HandleSessionStateChanged;
        }
        
        private bool IsGameOver(SessionStateType stateType) 
            => stateType > SessionStateType.Playing;

        private bool CanStartTimer(SessionStateType stateType) 
            => stateType == SessionStateType.Playing;
        
        private void HandleSessionStateChanged(SessionStateType stateType)
        {
            if (!CanStartTimer(stateType)) return;
            _timerService.Start();
        }
        
        private void HandlePausePressed()
        {
            _isPaused = !_isPaused;
            
            if (_isPaused) PauseGame();
            else ContinueGame();
        }

        public void Dispose() => Unsubscribe();
    }
}