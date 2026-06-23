using System;

namespace Minesweeper.Runtime.Infrastructure
{
    public class TimerService : ITimerService
    {
        private float _elapsed;
        private bool _running;
 
        public event Action<float> OnTick;
 
        public void Start() => _running = true;
        public void Stop() => _running = false;
 
        public void Reset()
        {
            _running = false;
            _elapsed = 0;
            OnTick?.Invoke(_elapsed);
        }
 
        public void Tick(float deltaTime)
        {
            if (!_running) return;
 
            _elapsed += deltaTime;
            OnTick?.Invoke(_elapsed);
        }
    }
}