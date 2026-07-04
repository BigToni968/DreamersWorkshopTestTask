using UnityEngine;

namespace TestTask
{
    [CreateAssetMenu(menuName = "Game/Config/Inventory")]
    public class InventoryData : ScriptableObject
    {
        [field: SerializeField] public Sprite Key { get; private set; }
        [field: SerializeField] public Sprite Necklace { get; private set; }
    }
}