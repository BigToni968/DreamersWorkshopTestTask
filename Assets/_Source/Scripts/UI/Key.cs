using TestTask.ReadOnly;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using Naninovel;

namespace TestTask.UI
{
    public class Key : MonoBehaviour
    {
        [SerializeField] private KeyData data;
        [SerializeField] private Button keyButton;

        private float _startPosY;
        private IScriptPlayer _script;

        private void Start()
        {
            _script = Engine.GetService<IScriptPlayer>();
            keyButton.onClick.AddListener(Action);
            _startPosY = keyButton.transform.position.y;
           keyButton.transform.DOMoveY(_startPosY + data.Offset, data.Duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }

        private void Action()
        {
            _script.PreloadAndPlayAsync(data.NameScript, data.NameLabel);
        }

        private void OnDestroy()
        {
            keyButton.onClick.RemoveListener(Action);
            keyButton.transform.DOKill();
        }
    }
}