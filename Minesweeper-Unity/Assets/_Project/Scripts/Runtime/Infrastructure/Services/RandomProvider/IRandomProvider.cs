namespace Minesweeper.Runtime.Infrastructure
{
    public interface IRandomProvider
    {
        int Next(int minInclusive, int maxExclusive);
    }
}