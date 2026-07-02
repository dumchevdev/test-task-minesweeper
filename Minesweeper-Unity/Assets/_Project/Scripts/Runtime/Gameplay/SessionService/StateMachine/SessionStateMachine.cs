using System.Collections.Generic;

namespace Minesweeper.Runtime.Gameplay
{
    public class SessionStateMachine
    {
        public BaseSessionState Current { get; private set; }
    
        private readonly Dictionary<SessionStateType, BaseSessionState> _handlers;
    
        public SessionStateMachine(GridData gridData, MinesPlacer minesPlacer, FloodFiller floodFiller)
        {
            _handlers = new Dictionary<SessionStateType, BaseSessionState>
            {
                { SessionStateType.WaitingFirstClick, new WaitingFirstClickState(minesPlacer) },
                { SessionStateType.Playing,  new PlayingState(floodFiller) },
                { SessionStateType.Won, new WonState() },
                { SessionStateType.Lost, new LostState(gridData) }
            };
        }
    
        public void TransitionTo(SessionStateType stateType)
        {
            Current = _handlers[stateType];
            Current.OnEnter();
        }
    }
}