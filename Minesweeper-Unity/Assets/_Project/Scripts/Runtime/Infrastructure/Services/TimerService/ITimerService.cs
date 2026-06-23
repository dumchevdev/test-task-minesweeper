using System;

namespace Minesweeper.Runtime.Infrastructure
{
    public interface ITimerService
    {
        void Start();
        void Stop();
        void Reset();
        void Tick(float deltaTime);
        
        event Action<float> OnTick;
    }
}