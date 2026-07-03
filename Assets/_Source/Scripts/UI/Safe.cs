using TestTask.ReadOnly;
using UnityEngine.UI;
using UnityEngine;
using Naninovel;

namespace TestTask.UI
{
    public class Safe : MonoBehaviour
    {
        [SerializeField] private SafeData data;
        [SerializeField] private Canvas self;
        [SerializeField] private Button safeButton;
        [SerializeField] private Image safeImage;
        [SerializeField] private Transform necklacePoint;
        
        private Button _necklace;
        private ICustomVariableManager _variables;
        private IScriptPlayer _script;
        private bool _isOpen;
        private bool _playerHasNecklace;
        private bool _wiseManHasNecklace;

        private void Start()
        {
            _script = Engine.GetService<IScriptPlayer>();
            _variables = Engine.GetService<ICustomVariableManager>();
            self.worldCamera = Engine.GetService<ICameraManager>().UICamera;
            _variables.TryGetVariableValue(data.CustomVarSafe, out _isOpen);
            _variables.TryGetVariableValue(data.CustomVarWiseMan, out _wiseManHasNecklace);
            _variables.TryGetVariableValue(data.CustomVarPlayer, out _playerHasNecklace);
            safeImage.sprite = _isOpen ? data.SafeOpen : data.SafeClose;
            var isSpawnNecklace = _isOpen && !_playerHasNecklace && !_wiseManHasNecklace;
            if (isSpawnNecklace)
            {
                _necklace = Instantiate(data.NecklacePrefab, necklacePoint);
                _necklace.onClick.AddListener(TakeNecklace);
            }

            safeButton.interactable = _necklace == null;
            safeButton.onClick.AddListener(Action);
        }

        private void OnDestroy()
        {
            safeButton.onClick.RemoveListener(Action);
            _necklace?.onClick.RemoveListener(TakeNecklace);
        }

        private void Action()
        {
            _variables.TryGetVariableValue(data.CustomVarKey, out bool hasKey);
            if (!_isOpen && hasKey)
            {
                _isOpen = true;
                hasKey = false;
                _variables.SetVariableValue(data.CustomVarSafe,_isOpen.ToString());
                _variables.SetVariableValue(data.CustomVarKey,hasKey.ToString());
                safeButton.interactable = !_isOpen;
                safeImage.sprite = data.SafeOpen;
                _necklace = Instantiate(data.NecklacePrefab, necklacePoint);
                _necklace.onClick.AddListener(TakeNecklace);
            }
            else if (!_isOpen && !hasKey)
            {
                _script.PreloadAndPlayAsync(data.NameScript, data.NameLabel);
            }
        }

        private void TakeNecklace()
        {
            _necklace.onClick.RemoveListener(TakeNecklace);
            Destroy(_necklace.gameObject);
            _playerHasNecklace = true;
            _variables.SetVariableValue(data.CustomVarPlayer,_playerHasNecklace.ToString());
        }
    }
}