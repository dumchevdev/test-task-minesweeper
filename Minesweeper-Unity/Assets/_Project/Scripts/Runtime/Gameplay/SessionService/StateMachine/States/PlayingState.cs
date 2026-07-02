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
                sessionState.TransitionTo(SessionStateType.Lost);
                return;
            }
        
            var openedCount = _floodFiller.OpenFrom(cellData);
            sessionState.GridData.DecreaseCellsRemaining(openedCount);

            if (sessionState.GridData.CellsRemaining <= 0)
                sessionState.TransitionTo(SessionStateType.Won);
        }
    }
}