using UnityEngine;
using VContainer;
using VContainer.Unity;
using Minesweeper.Runtime.UI;

namespace Minesweeper.Runtime.Infrastructure
{
    public class BootstrapScope : LifetimeScope
    {
        [SerializeField] private UIRoot uiRoot;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);
            builder.Register<IConfigsProvider, ConfigsProvider>(Lifetime.Singleton);
            
            new GameplayInstaller().Install(builder);
            new UIInstaller(uiRoot).Install(builder);
   
            builder.RegisterEntryPoint<BootstrapFlow>();
        }
    }
}