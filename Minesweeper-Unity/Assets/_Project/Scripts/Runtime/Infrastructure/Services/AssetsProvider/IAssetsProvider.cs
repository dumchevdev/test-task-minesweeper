using UnityEngine;

namespace Minesweeper.Runtime.Infrastructure
{
    public interface IAssetsProvider
    {
        T Load<T>(string address) where T : Object;
    }
}