using UnityEngine;

namespace Minesweeper.Runtime.Infrastructure
{
    public class AssetsProvider : IAssetsProvider
    {
        public T Load<T>(string address) where T : Object => 
            Resources.Load<T>(address);
    }
}