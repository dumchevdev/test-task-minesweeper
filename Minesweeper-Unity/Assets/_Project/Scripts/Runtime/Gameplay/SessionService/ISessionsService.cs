using System;

namespace Minesweeper.Runtime.Gameplay
{
    public interface ISessionsService
    {
        SessionState CurrentSession { get; }
        
        void CreateNewSession();
        void ResetSession();
        
        event Action<GridData> OnSessionStarted;
        event Action<SessionStateType> OnSessionStateChanged;
    }
}