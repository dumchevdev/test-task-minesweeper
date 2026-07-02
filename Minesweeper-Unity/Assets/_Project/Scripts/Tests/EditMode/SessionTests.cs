using NUnit.Framework;
using Minesweeper.Runtime.Gameplay;

namespace Minesweeper.Tests.EditMode
{
    public class SessionTests
    {
        private SessionState _session;

        [SetUp]
        public void SetUp()
        {
            var grid = TestsHelper.CreateGridData();

            var randomProvider = TestsHelper.CreateRandomProvider();
            _session = new SessionState(grid, randomProvider);
        }

        [Test]
        public void WaitingFirstClick_OpenCell_TransitionsToPlaying()
        {
            Assert.AreEqual(SessionStateType.WaitingFirstClick, _session.StateType);

            var cellData = _session.GridData.GetCell(0, 0);
            _session.OpenCell(cellData);
            
            Assert.AreEqual(SessionStateType.Playing, _session.StateType);
        }
        
        [Test]
        public void WaitingFirstClick_OpenCell_FirstCellIsNotMine()
        {
            var cellData = _session.GridData.GetCell(4, 4);
            _session.OpenCell(cellData);
            
            Assert.IsFalse(cellData.IsMine);
        }

        [Test]
        public void WaitingFirstClick_ToggleFlag_TransitionsToPlaying()
        {
            var cellData = _session.GridData.GetCell(4, 4);
            _session.ToggleFlag(cellData);

            Assert.AreEqual(SessionStateType.Playing, _session.StateType);
        }

        
        [Test]
        public void Playing_OpenCell_WithFlag_DoesNotOpen()
        {
            _session.TransitionTo(SessionStateType.Playing);

            var cellData = _session.GridData.GetCell(1, 1);
            cellData.ToggleFlag();
            
            _session.OpenCell(cellData);
            Assert.IsFalse(cellData.IsOpen);
        }

        [Test]
        public void Playing_OpenCell_Mine_TransitionsToLost()
        {
            _session.TransitionTo(SessionStateType.Playing);

            var cellData = _session.GridData.GetCell(1, 1);
            cellData.PlaceMine();
            
            _session.OpenCell(cellData);
            Assert.AreEqual(SessionStateType.Lost, _session.StateType);
        }

        [Test]
        public void Playing_OpenCell_AllNonMines_TransitionsToWon()
        {
            _session.TransitionTo(SessionStateType.Playing);

            var cellData = _session.GridData.GetCell(0, 0);
            _session.OpenCell(cellData);
            
            Assert.AreEqual(SessionStateType.Won, _session.StateType);
        }
    }
}