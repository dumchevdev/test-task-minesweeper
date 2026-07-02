using System;

namespace Minesweeper.Runtime.Gameplay
{
    public interface ISessionsService
    {
        void CreateNewSession();
        void ResetSession();
        
        void OpenCell(CellData cellData);
        void ToggleFlag(CellData cellData);
        
        event Action<GridData> OnSessionStarted;
        event Action<SessionStateType> OnSessionStateChanged;
    }
}