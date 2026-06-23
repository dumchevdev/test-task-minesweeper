namespace Minesweeper.Runtime.UI
{
    public interface ICellViewFactory
    {
        CellView Create(int x, int y);
    }
}