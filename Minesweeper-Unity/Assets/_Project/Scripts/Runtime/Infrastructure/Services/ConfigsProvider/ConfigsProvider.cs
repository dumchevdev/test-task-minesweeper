using Minesweeper.Runtime.Common;
using Minesweeper.Runtime.Configs;

namespace Minesweeper.Runtime.Infrastructure
{
    public class ConfigsProvider : IConfigsProvider
    {
        private readonly IAssetProvider _assetProvider;
        
        public GridConfig GridConfig { get; private set; }
        public CellViewConfig CellViewConfig { get; private set; }

        public ConfigsProvider(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void Initialize()
        {
            GridConfig = _assetProvider.Load<GridConfig>(Constants.Resources.GridConfigPath);
            CellViewConfig = _assetProvider.Load<CellViewConfig>(Constants.Resources.CellViewConfigPath);
        }
    }
}