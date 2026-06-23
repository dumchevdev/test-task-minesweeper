using Minesweeper.Runtime.Configs;

namespace Minesweeper.Runtime.Infrastructure
{
    public interface IConfigsProvider
    {
        void Initialize();
        GridConfig GridConfig { get; }
        CellViewConfig CellViewConfig { get; }
    }
}