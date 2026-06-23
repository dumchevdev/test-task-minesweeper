using UnityEngine;

namespace Minesweeper.Runtime.Infrastructure
{
    public class AssetProvider : IAssetProvider
    {
        public T Load<T>(string address) where T : Object
        {
            return Resources.Load<T>(address);
        }
    }
}