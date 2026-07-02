namespace Minesweeper.Runtime.Gameplay
{
    public abstract class BaseSessionState
    {
        public abstract SessionStateType StateType { get; }
 
        public virtual void OnEnter() { }
        public virtual void OpenCell(SessionState sessionState, CellData cellData) { }
        public virtual void ToggleFlag(SessionState sessionState, CellData cellData)
        {
            if (cellData.IsOpen) return;
            cellData.ToggleFlag();
        }
    }
}