namespace Minesweeper.Runtime.Gameplay
{
    public enum SessionStateType
    {
        WaitingFirstClick,
        Playing,
        Won,
        Lost
    }
    
    public static class SessionStateTypeExtensions
    {
        public static bool IsPlaying(this SessionStateType stateType) =>
            stateType == SessionStateType.Playing;

        public static bool IsFinished(this SessionStateType stateType) =>
            stateType == SessionStateType.Won ||
            stateType == SessionStateType.Lost;
    }
}