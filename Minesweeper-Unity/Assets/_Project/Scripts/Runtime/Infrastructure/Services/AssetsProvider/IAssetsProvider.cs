using UnityEngine;

namespace Minesweeper.Runtime.Infrastructure
{
    public interface IAssetProvider
    {
        T Load<T>(string address) where T : Object;
    }
}