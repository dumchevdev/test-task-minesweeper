using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = System.Random;
using Minesweeper.Runtime.UI;

namespace Minesweeper.Runtime.Infrastructure
{
    public class BootstrapScope : LifetimeScope
    {
        [SerializeField] private UIRoot uiRoot;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IAssetsProvider, AssetsProvider>(Lifetime.Singleton);
            builder.Register<IConfigsProvider, ConfigsProvider>(Lifetime.Singleton);
            builder.Register<IRandomProvider>(_ => 
                new RandomProvider(new Random()), Lifetime.Singleton);
            builder.Register<IInputsService, InputsService>(Lifetime.Singleton);

            new GameplayInstaller().Install(builder);
            new UIInstaller(uiRoot).Install(builder);
   
            builder.RegisterEntryPoint<BootstrapFlow>();
        }
    }
}