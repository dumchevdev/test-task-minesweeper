using UnityEngine;

namespace Minesweeper.Runtime.UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private MainMenuWindow mainMenuWindow;
        [SerializeField] private HudWindow hudWindow;
        [SerializeField] private PauseWindow pauseWindow;
        [SerializeField] private GameOverWindow gameOverWindow;

        public MainMenuWindow MainMenu => mainMenuWindow;
        public HudWindow Hud => hudWindow;
        public PauseWindow Pause => pauseWindow;
        public GameOverWindow GameOverWindow => gameOverWindow;
    }
}