namespace Minesweeper.Runtime.Gameplay
{
    public enum GameFlowState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }
    
    public static class GameFlowStateExtensions
    {
        public static bool CanPause(this GameFlowState state) =>
            state == GameFlowState.Playing || 
            state == GameFlowState.GameOver;
    }
}