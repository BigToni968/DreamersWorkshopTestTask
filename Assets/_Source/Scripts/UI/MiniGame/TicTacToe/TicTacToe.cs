using TestTask.ReadOnly;
using UnityEngine.UI;
using UnityEngine;

namespace TestTask.UI.MiniGame
{
    public enum User
    {
        Player,
        Bot
    }

    public class TicTacToe : MonoBehaviour
    {
        [SerializeField] private TicTacToeData data;
        [SerializeField] private Transform content;
        [SerializeField] private Image rayBlock;
        [SerializeField] private Button closeButton;

        public Vector2Int Size => data.Size;

        private TicTacToeCell[,] _cells;
        private MiniGameService _gameService;

        public void Init(MiniGameService gameService)
        {
            closeButton.onClick.AddListener(Close);
            _gameService = gameService;
            _cells = new TicTacToeCell[data.Size.y, data.Size.x];

            for (var y = 0; y < data.Size.y; y++)
            {
                for (var x = 0; x < data.Size.x; x++)
                {
                    var posInit = new Vector2Int(x, y);
                    var cellInit = Instantiate(data.CellPrefab, content);
                    cellInit.CellButton.onClick.AddListener(() => TouchCell(posInit, User.Player));
                    _cells[y, x] = cellInit;
                }
            }
        }

        public void TouchCell(Vector2Int pos, User user)
        {
            var cell = _cells[pos.y, pos.x];
            cell.CellButton.onClick.RemoveAllListeners();
            cell.CellButton.interactable = false;
            cell.CellIcon.sprite = user == User.Player ? data.Cross : data.Zero;
            cell.CellIcon.gameObject.SetActive(true);
            rayBlock.gameObject.SetActive(user == User.Player);
            if (user == User.Player)
                _gameService.StepPlayer(pos);
        }

        private void Close()
        {
            _gameService.Hide();
        }
    }
}