using System.Collections.Generic;
using TestTask.ReadOnly;
using UnityEngine.UI;
using UnityEngine;
using Naninovel;

namespace TestTask.UI
{
    public class Navigation : MonoBehaviour
    {
        [SerializeField] private Button left;
        [SerializeField] private Button right;
        [SerializeField] private NavigationData leftData;
        [SerializeField] private NavigationData rightData;

        private ICustomVariableManager _variables;
        private string _curentLocation;
        private IScriptPlayer _script;
        private Dictionary<int, int> _sides;
        private const string CUSTOM_VARIABLE = "CurentLocation";

        private void Start()
        {
            left.onClick.AddListener(LeftNext);
            right.onClick.AddListener(RightNext);
            _variables = Engine.GetService<ICustomVariableManager>();
            _variables.OnVariableUpdated += HandleVariableUpdated;
            _script = Engine.GetService<IScriptPlayer>();
            _sides = new(2);
            CheckSides();
        }

        private void OnDestroy()
        {
            left.onClick.RemoveListener(LeftNext);
            right.onClick.RemoveListener(RightNext);
            _variables.OnVariableUpdated -= HandleVariableUpdated;
        }

        private void CheckSides()
        {
            _curentLocation = _variables.GetVariableValue(CUSTOM_VARIABLE); 
            left.gameObject.SetActive(leftData.IsShowSide(_curentLocation,out var index));
            _sides[left.name.GetHashCode()] = index;
            right.gameObject.SetActive(rightData.IsShowSide(_curentLocation,out index));
            _sides[right.name.GetHashCode()] = index;
        }
        
        private void HandleVariableUpdated(CustomVariableUpdatedArgs args)
        {
            if (args.Name.GetHashCode() == CUSTOM_VARIABLE.GetHashCode())
                CheckSides();
        }

        private async void LeftNext()
        {
            await _script.PreloadAndPlayAsync(leftData.GetNextLocation(_sides[left.name.GetHashCode()]));
        }

        private async void RightNext()
        {
            await _script.PreloadAndPlayAsync(rightData.GetNextLocation(_sides[right.name.GetHashCode()]));
        }
    }
}