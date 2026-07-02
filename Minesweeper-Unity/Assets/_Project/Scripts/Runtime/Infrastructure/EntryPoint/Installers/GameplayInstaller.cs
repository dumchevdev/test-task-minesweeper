using VContainer;
using VContainer.Unity;
using Minesweeper.Runtime.Gameplay;

namespace Minesweeper.Runtime.Infrastructure
{
    public class GameplayInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ITimerService, TimerService>(Lifetime.Singleton);
            builder.Register<ISessionsService, SessionsService>(Lifetime.Singleton);
            builder.Register<IGameFlowService, GameFlowService>(Lifetime.Singleton);
        }
    }
}