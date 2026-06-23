namespace Minesweeper.Runtime.UI
{
    public class UIRouter : IUIRouter
    {
        private readonly UIRoot _uiRoot;
        public UIRouter(UIRoot uiRoot) => _uiRoot = uiRoot;

        public void ShowMainMenu()
        {
            HideAll();
            _uiRoot.MainMenu.Show();
        }

        public void ShowHud()
        {
            HideAll();
            _uiRoot.Hud.Show();
        }

        public void ShowPause()
        {
            HideAll();
            _uiRoot.Pause.Show();
        }

        public void ShowGameOver()
        {
            HideAll();
            _uiRoot.Hud.Show();
            _uiRoot.GameOverWindow.Show();
        }

        private void HideAll()
        {
            _uiRoot.MainMenu.Hide();
            _uiRoot.Hud.Hide();
            _uiRoot.Pause.Hide();
            _uiRoot.GameOverWindow.Hide();
        }
    }
}