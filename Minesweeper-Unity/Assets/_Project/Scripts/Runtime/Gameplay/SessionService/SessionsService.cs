using System;
using Minesweeper.Runtime.Infrastructure;

namespace Minesweeper.Runtime.Gameplay
{
    public class SessionsService : ISessionsService, IDisposable
    {
        private readonly IConfigsProvider _configsProvider;
        private readonly Random _sessionRandom;
 
        public SessionState CurrentSession { get; private set; }
 
        public event Action<GridData> OnSessionStarted;
        public event Action<SessionStateType> OnSessionStateChanged;
 
        public SessionsService(IConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
            _sessionRandom = new Random();
        }
 
        public void CreateNewSession()
        {
            ResetSession();

            var gridConfig = _configsProvider.GridConfig;
            var gridData = new GridData(
                gridConfig.Width,
                gridConfig.Height, 
                gridConfig.MineCount);
            
            CurrentSession = new SessionState(gridData, _sessionRandom);
            CurrentSession.OnSessionStateChanged += HandleSessionStateChanged;
            
            OnSessionStarted?.Invoke(CurrentSession.GridData);
        }

        public void ResetSession()
        {
            if (CurrentSession == null) return;
            CurrentSession.OnSessionStateChanged -= HandleSessionStateChanged;
            CurrentSession = null;
        }
        
        private void HandleSessionStateChanged(SessionStateType stateType) 
            => OnSessionStateChanged?.Invoke(stateType);
 
        public void Dispose() => ResetSession();
    }
}