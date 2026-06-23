namespace Minesweeper.Runtime.Gameplay
{
    public class PlayingState : BaseSessionState
    {
        public override SessionStateType StateType => SessionStateType.Playing;
 
        private readonly FloodFiller _floodFiller;
 
        public PlayingState(FloodFiller floodFiller)
        {
            _floodFiller = floodFiller;
        }
 
        public override void OpenCell(SessionState sessionState, CellData cellData)
        {
            if (!cellData.IsClosed || cellData.IsFlagged) 
                return;
 
            if (cellData.IsMine)
            {
                cellData.Explode();
                sessionState.TransitionTo(SessionStateType.Finished_Lost);
                return;
            }
 
            _floodFiller.FillFrom(cellData);

            if (IsWon(sessionState))
                sessionState.TransitionTo(SessionStateType.Finished_Won);
        }
 
        private static bool IsWon(SessionState sessionState)
        {
            foreach (var cell in sessionState.GridData.GetAllCells())
            {
                if (!cell.IsMine && !cell.IsOpen)
                    return false;
            }

            return true;
        }
    }
}