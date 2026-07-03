using UnityEngine;
using Naninovel;

namespace TestTask.UI
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private Canvas self;

        private void Start()
        {
            self.worldCamera = Engine.GetService<ICameraManager>().Camera;
        }
    }
}