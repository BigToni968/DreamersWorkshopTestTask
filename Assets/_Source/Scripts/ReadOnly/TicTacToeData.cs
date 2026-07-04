using TestTask.UI.MiniGame;
using UnityEngine;

namespace TestTask.ReadOnly
{
    [CreateAssetMenu(menuName = "Game/Config/MiniGame/TicTacToe")]
    public class TicTacToeData : ScriptableObject
    {
        [field: SerializeField] public Sprite Cross { get; private set; }
        [field: SerializeField] public Sprite Zero { get; private set; }
        [field:SerializeField] public Vector2Int Size { get; private set; }
        [field: SerializeField] public TicTacToeCell CellPrefab { get; private set; }
    }
}