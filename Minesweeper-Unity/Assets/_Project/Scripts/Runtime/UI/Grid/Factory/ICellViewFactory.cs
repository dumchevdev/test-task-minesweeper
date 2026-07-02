namespace Minesweeper.Runtime.UI
{
    public interface ICellViewFactory
    {
        CellView Create();
        void Destroy(CellView cellView);
    }
}