namespace Minesweeper.Runtime.Gameplay
{
    public class LostState : BaseSessionState
    {
        public override SessionStateType StateType => SessionStateType.Lost;

        private readonly GridData _gridData;

        public LostState(GridData gridData)
        {
            _gridData = gridData;
        }

        public override void OnEnter()
        {
            foreach (var cell in _gridData.GetAllCells())
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