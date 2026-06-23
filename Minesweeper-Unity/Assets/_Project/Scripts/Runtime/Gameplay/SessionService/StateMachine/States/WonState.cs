namespace Minesweeper.Runtime.Gameplay
{
    public class WonState : BaseSessionState
    {
        public override SessionStateType StateType => SessionStateType.Finished_Won;
        public override void OpenCell(SessionState sessionState, CellData cellData) { }
        public override void ToggleFlag(SessionState sessionState, CellData cellData) { }
    }
}