using UnityEngine;

namespace TestTask.ReadOnly
{
    [CreateAssetMenu(menuName = "Game/Config/NPC/WiseMan")]
    public class WiseManData : ScriptableObject
    {
        [field: SerializeField] public string NameScript { get; private set; } = "LocationVillage";
        [field: SerializeField] public string NameLabel { get; private set; } = "wise_man";
    }
}