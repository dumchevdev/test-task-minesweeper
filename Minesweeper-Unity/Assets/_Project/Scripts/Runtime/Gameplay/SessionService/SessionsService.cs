using System;
using Minesweeper.Runtime.Infrastructure;

namespace Minesweeper.Runtime.Gameplay
{
    public class SessionsService : ISessionsService, IDisposable
    {
        private readonly IConfigsProvider _configsProvider;
        private readonly IRandomProvider _randomProvider;

        private SessionState _currentSession;

        public event Action<GridData> OnSessionStarted;
        public event Action<SessionStateType> OnSessionStateChanged;
 
        public SessionsService(IConfigsProvider configsProvider, IRandomProvider randomProvider)
        {
            _configsProvider = configsProvider;
            _randomProvider = randomProvider;
        }
 
        public void CreateNewSession()
        {
            ResetSession();

            var gridConfig = _configsProvider.GridConfig;
            var gridData = new GridData(
                gridConfig.Width,
                gridConfig.Height, 
                gridConfig.MinesCount);
            
            _currentSession = new SessionState(gridData, _randomProvider);
            _currentSession.OnSessionStateChanged += HandleSessionStateChanged;
            
            _currentSession.Start();
            OnSessionStarted?.Invoke(_currentSession.GridData);
        }

        public void ResetSession()
        {
            if (_currentSession == null) 
                return;
            
            _currentSession.OnSessionStateChanged -= HandleSessionStateChanged;
            _currentSession = null;
        }

        public void OpenCell(CellData cellData) => 
            _currentSession?.OpenCell(cellData);
        
        public void ToggleFlag(CellData cellData) => 
            _currentSession?.ToggleFlag(cellData);
        
        private void HandleSessionStateChanged(SessionStateType stateType) => 
            OnSessionStateChanged?.Invoke(stateType);
 
        public void Dispose() => ResetSession();
    }
}