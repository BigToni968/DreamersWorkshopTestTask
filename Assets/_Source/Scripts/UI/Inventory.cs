using UnityEngine.UI;
using UnityEngine;

namespace TestTask.UI
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private InventoryData data;
        [SerializeField] private Image icon;

        public void SetIcon(string name)
        {
            var hasName = name.GetHashCode();
            if (hasName == nameof(data.Key).ToLower().GetHashCode())
                icon.sprite = data.Key;
            else if (hasName == nameof(data.Necklace).ToLower().GetHashCode())
                icon.sprite = data.Necklace;
            else
                icon.sprite = null;
        }
    }
}