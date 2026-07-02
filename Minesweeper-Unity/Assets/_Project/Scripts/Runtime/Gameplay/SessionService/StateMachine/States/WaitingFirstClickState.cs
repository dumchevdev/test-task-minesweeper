namespace Minesweeper.Runtime.Gameplay
{
    public class WaitingFirstClickState : BaseSessionState
    {
        public override SessionStateType StateType => SessionStateType.WaitingFirstClick;
 
        private readonly MinesPlacer _minesPlacer;
 
        public WaitingFirstClickState(MinesPlacer minesPlacer)
        {
            _minesPlacer = minesPlacer;
        }
 
        public override void OpenCell(SessionState sessionState, CellData cellData)
        {
            _minesPlacer.PlaceMines(cellData);
            sessionState.TransitionTo(SessionStateType.Playing);
            sessionState.OpenCell(cellData);
        }
 
        public override void ToggleFlag(SessionState sessionState, CellData cellData)
        {
            _minesPlacer.PlaceMines();
            sessionState.TransitionTo(SessionStateType.Playing);
            sessionState.ToggleFlag(cellData);
        }
    }
}