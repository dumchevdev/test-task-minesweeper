using System;

namespace Minesweeper.Runtime.Gameplay
{
    public interface IGameFlowService : IDisposable
    {
        void StartNewGame();
        void PauseGame();
        void ContinueGame();
        void ExitToMainMenu();
        void ShowGameOver();
        void Tick(float deltaTime);
    }
}