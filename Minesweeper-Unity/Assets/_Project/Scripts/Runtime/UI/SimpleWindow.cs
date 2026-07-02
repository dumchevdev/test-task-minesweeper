using UnityEngine;

namespace Minesweeper.Runtime.UI
{
    public abstract class SimpleWindow : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}