using System.Collections.Generic;

namespace Minesweeper.Runtime.Gameplay
{
    public enum SessionStateType
    {
        WaitingFirstClick,
        Playing,
        Finished_Won,
        Finished_Lost
    }
    
    public class SessionStateMachine
    {
        public BaseSessionState Current { get; private set; }
    
        private readonly Dictionary<SessionStateType, BaseSessionState> _handlers;
    
        public SessionStateMachine(SessionState sessionState, 
            MinesPlacer minesPlacer, FloodFiller floodFiller)
        {
            _handlers = new Dictionary<SessionStateType, BaseSessionState>
            {
                { SessionStateType.WaitingFirstClick, new WaitingFirstClickState(minesPlacer) },
                { SessionStateType.Playing,  new PlayingState(floodFiller) },
                { SessionStateType.Finished_Won, new WonState() },
                { SessionStateType.Finished_Lost, new LostState(sessionState) }
            };
        }
    
        public void TransitionTo(SessionStateType stateType)
        {
            Current = _handlers[stateType];
            Current.OnEnter();
        }
    }
}