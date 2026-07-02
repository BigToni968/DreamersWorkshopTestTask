using TestTask.ReadOnly;
using UnityEngine.UI;
using UnityEngine;
using Naninovel;

namespace TestTask.UI
{
    public class WiseManButton : MonoBehaviour
    {
        [SerializeField] private Button self;
        [SerializeField] private WiseManData data;

        private void Start()
        {
            self.onClick.AddListener(Click);
        }

        private void OnDestroy()
        {
            self.onClick.RemoveListener(Click);
        }

        private async void Click()
        {
            var script = Engine.GetService<IScriptPlayer>();

            await script.PreloadAndPlayAsync(data.NameScript, data.NameLabel);
        }
    }
}