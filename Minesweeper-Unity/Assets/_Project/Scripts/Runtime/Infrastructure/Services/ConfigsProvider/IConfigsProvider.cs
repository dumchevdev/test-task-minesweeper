using Minesweeper.Runtime.Configs;

namespace Minesweeper.Runtime.Infrastructure
{
    public interface IConfigsProvider
    {
        GridConfig GridConfig { get; }
        CellViewConfig CellViewConfig { get; }
        
        void Initialize();
    }
}