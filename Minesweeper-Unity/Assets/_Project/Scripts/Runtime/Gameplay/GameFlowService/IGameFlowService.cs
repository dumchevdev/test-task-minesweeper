using System;

namespace Minesweeper.Runtime.Gameplay
{
    public interface IGameFlowService : IDisposable
    {
        void StartGame();
        void Tick(float deltaTime);

        void StartNewSession();
        void PauseGame();
        void ContinueGame();
        void ExitToMainMenu();
        
        event Action<SessionStateType> OnGameFinished;
    }
}