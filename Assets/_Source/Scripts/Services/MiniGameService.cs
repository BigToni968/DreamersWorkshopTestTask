using TestTask.UI.MiniGame;
using System.Collections;
using UnityEngine;
using Naninovel;

namespace TestTask
{
    [InitializeAtRuntime]
    public sealed class MiniGameService : IEngineService
    {
        private const string MINIGAME_PREFAB_NAME = "minigame";
        private const char PLAYER_SYMBOL = 'x';
        private const char BOT_SYMBOL = 'o';
        private const string LOCATION = "altar";
        private const string LABEL_PLAYER_WIN = MINIGAME_PREFAB_NAME + "Win";
        private const string LABEL_PLAYER_LOSE = MINIGAME_PREFAB_NAME + "Lose";
        private const string LABEL_PLAYER_TIE = MINIGAME_PREFAB_NAME + "Tie";

        private ISpawnManager _spawner;
        private IScriptPlayer _script;
        private TicTacToe _minigame;
        private char[,] _cells;
        private int _emptyCell;

        public UniTask InitializeServiceAsync()
        {
            _spawner = Engine.GetService<ISpawnManager>();
            _script = Engine.GetService<IScriptPlayer>();
            return UniTask.CompletedTask;
        }

        public void ResetService()
        {
        }

        public void DestroyService()
        {
            Hide();
        }

        public void Show()
        {
            var task = _spawner.SpawnAsync(MINIGAME_PREFAB_NAME);
            if (!task.IsCompleted)
                return;
            task.Result.GameObject.TryGetComponent(out _minigame);
            _emptyCell = _minigame.Size.x * _minigame.Size.y;
            _cells = new char[_minigame.Size.y, _minigame.Size.x];
        }

        public void Hide()
        {
            if (_minigame != null)
                _spawner.DestroySpawned(MINIGAME_PREFAB_NAME);
        }

        public void StepPlayer(Vector2Int pos)
        {
            _cells[pos.y, pos.x] = PLAYER_SYMBOL;
            _emptyCell--;

            if (_emptyCell <= 0)
            {
                _script.PreloadAndPlayAsync(LOCATION, LABEL_PLAYER_TIE);
                return;
            }
            else if (IsWin(PLAYER_SYMBOL))
            {
                _script.PreloadAndPlayAsync(LOCATION, LABEL_PLAYER_WIN);
                return;
            }
            
            _minigame.StartCoroutine(FindPosBot());
        }

        private bool IsWin(char user)
        {
            var countFilledCells = 0;
            //vertical
            for (var x = 0; x < _minigame.Size.x; x++)
            {
                for (var y = 0; y < _minigame.Size.y; y++)
                {
                    if (_cells[y, x] == user)
                        countFilledCells++;
                }

                if (countFilledCells == _minigame.Size.y)
                    return true;
                countFilledCells = 0;
            }

            countFilledCells = 0;
            //horizontal
            for (var y = 0; y < _minigame.Size.y; y++)
            {
                for (var x = 0; x < _minigame.Size.x; x++)
                {
                    if (_cells[y, x] == user)
                        countFilledCells++;
                }

                if (countFilledCells == _minigame.Size.y)
                    return true;
                countFilledCells = 0;
            }

            countFilledCells = 0;
            // 0.0 -> 2.2
            for (var indexCell = 0; indexCell < _minigame.Size.y; indexCell++)
            {
                if (_cells[indexCell, indexCell] == user)
                    countFilledCells++;
                if (countFilledCells == _minigame.Size.y)
                    return true;
            }

            countFilledCells = 0;
            // 2.2 -> 0.0
            for (var indexCell = _minigame.Size.y - 1; indexCell >= 0; indexCell--)
            {
                if (_cells[indexCell, indexCell] == user)
                    countFilledCells++;
                if (countFilledCells == _minigame.Size.y)
                    return true;
            }

            return false;
        }

        private void StepBot(Vector2Int pos)
        {
            _cells[pos.y, pos.x] = BOT_SYMBOL;
            _minigame.TouchCell(pos, User.Bot);
            _emptyCell--;
            if (_emptyCell <= 0)
                _script.PreloadAndPlayAsync(LOCATION, LABEL_PLAYER_TIE);
            else if (IsWin(BOT_SYMBOL))
                _script.PreloadAndPlayAsync(LOCATION, LABEL_PLAYER_LOSE);
        }

        private IEnumerator FindPosBot()
        {
            var pos = Vector2Int.zero;
            var loop = true;
            while (loop)
            {
                var cell = _cells[pos.y, pos.x];
                if (cell == PLAYER_SYMBOL || cell == BOT_SYMBOL)
                {
                    pos.x = Random.Range(0, _minigame.Size.x);
                    pos.y = Random.Range(0, _minigame.Size.y);
                    continue;
                }

                StepBot(pos);
                loop = false;
                yield return null;
            }
        }
    }
}