using System;
using Minesweeper.Runtime.Infrastructure;

namespace Minesweeper.Runtime.Gameplay
{
    public class SessionState
    {
        private readonly SessionStateMachine _stateMachine;
        
        public GridData GridData { get; }
        public SessionStateType StateType => _stateMachine.Current.StateType;
        
        public event Action<SessionStateType> OnSessionStateChanged;

        public SessionState(GridData gridData, IRandomProvider randomProvider)
        {
            GridData = gridData;
            
            var minesPlacer = new MinesPlacer(gridData, randomProvider);
            var emptyAreaExpander = new FloodFiller(gridData);
            _stateMachine = new SessionStateMachine(gridData, minesPlacer, emptyAreaExpander);
        }

        public void Start() => TransitionTo(SessionStateType.WaitingFirstClick);

        public void TransitionTo(SessionStateType stateType)
        {
            _stateMachine.TransitionTo(stateType);
            OnSessionStateChanged?.Invoke(stateType);
        }
 
        public void OpenCell(CellData cellData) => 
            _stateMachine.Current.OpenCell(this, cellData);

        public void ToggleFlag(CellData cellData) => 
            _stateMachine.Current.ToggleFlag(this, cellData);
    }
}