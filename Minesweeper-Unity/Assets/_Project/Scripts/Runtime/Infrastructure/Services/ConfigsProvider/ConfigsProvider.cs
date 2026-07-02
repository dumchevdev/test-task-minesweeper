using Minesweeper.Runtime.Common;
using Minesweeper.Runtime.Configs;

namespace Minesweeper.Runtime.Infrastructure
{
    public class ConfigsProvider : IConfigsProvider
    {
        private readonly IAssetsProvider _assetsProvider;
        
        public GridConfig GridConfig { get; private set; }
        public CellViewConfig CellViewConfig { get; private set; }

        public ConfigsProvider(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public void Initialize()
        {
            GridConfig = _assetsProvider.Load<GridConfig>(Constants.Resources.GridConfigPath);
            CellViewConfig = _assetsProvider.Load<CellViewConfig>(Constants.Resources.CellViewConfigPath);
        }
    }
}