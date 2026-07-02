using System;

namespace Minesweeper.Runtime.Infrastructure
{
    public interface IInputsService
    {
        event Action OnRestartPressed;
        event Action OnPausePressed;
    }
}