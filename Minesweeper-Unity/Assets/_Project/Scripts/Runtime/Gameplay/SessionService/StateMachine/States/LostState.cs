namespace Minesweeper.Runtime.Gameplay
{
    public class LostState : BaseSessionState
    {
        public override SessionStateType StateType => SessionStateType.Finished_Lost;

        private readonly SessionState _sessionState;

        public LostState(SessionState sessionState)
        {
            _sessionState = sessionState;
        }

        public override void OnEnter()
        {
            foreach (var cell in _sessionState.GridData.GetAllCells())
            {
                if (!cell.IsMine || cell.IsExploded) 
                    continue;
                
                cell.Open();
            }
        }

        public override void OpenCell(SessionState sessionState, CellData cellData) { }
        public override void ToggleFlag(SessionState sessionState, CellData cellData) { }
    }
}