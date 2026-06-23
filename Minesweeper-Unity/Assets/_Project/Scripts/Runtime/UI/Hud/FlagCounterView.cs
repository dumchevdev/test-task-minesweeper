using TMPro;
using UnityEngine;

namespace Minesweeper.Runtime.UI
{
    public class FlagCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
 
        public void UpdateCount(int remaining) 
            => label.text = remaining.ToString();
    }
}