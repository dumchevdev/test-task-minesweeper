namespace Minesweeper.Runtime.UI
{
    public interface IUIRouter
    {
        public void ShowMainMenu();
        public void ShowHud();
        public void ShowPause();
        public void ShowGameOver();
    }
}