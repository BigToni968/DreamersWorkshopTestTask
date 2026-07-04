using UnityEngine.UI;
using UnityEngine;

namespace TestTask.UI.MiniGame
{
    public class TicTacToeCell : MonoBehaviour
    {
        [field:SerializeField] public Button CellButton { get; private set; }
        [field: SerializeField] public Image CellIcon { get; private set; }
    }
}