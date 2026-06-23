using VContainer;
using VContainer.Unity;
using Minesweeper.Runtime.UI;

namespace Minesweeper.Runtime.Infrastructure
{
    public class UIInstaller : IInstaller
    {
        private readonly UIRoot _uiRoot;
 
        public UIInstaller(UIRoot uiRoot) =>
            _uiRoot = uiRoot;
        
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ICellViewFactory, CellViewFactory>(Lifetime.Singleton);
            
            builder.RegisterComponent(_uiRoot);
            builder.Register<IUIRouter, UIRouter>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<MainMenuPresenter>();
            builder.RegisterEntryPoint<HudPresenter>();
            builder.RegisterEntryPoint<PausePresenter>();
            builder.RegisterEntryPoint<GameOverPresenter>();
        }
    }
}