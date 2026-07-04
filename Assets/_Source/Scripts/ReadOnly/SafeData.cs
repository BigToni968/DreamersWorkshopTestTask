using UnityEngine.UI;
using UnityEngine;

namespace TestTask.ReadOnly
{
    [CreateAssetMenu(menuName = "Game/Config/Safe")]
    public class SafeData : ScriptableObject
    {
        [field: SerializeField] public Sprite SafeClose { get; private set; }
        [field: SerializeField] public Sprite SafeOpen { get; private set; }
        [field: SerializeField] public Button NecklacePrefab { get; private set; }
        [field: SerializeField] public string CustomVarSafe { get; private set; } = "SafeOpened";
        [field: SerializeField] public string CustomVarPlayer { get; private set; } = "PlayerHasNecklace";
        [field: SerializeField] public string CustomVarKey { get; private set; } = "PlayerHasKey";
        [field: SerializeField] public string CustomVarWiseMan { get; private set; } = "WiseManHasNecklace";
        [field: SerializeField] public string NameScript { get; private set; } = "castle_hall";
        [field: SerializeField] public string NameLabel { get; private set; } = "player";
        [field: SerializeField] public string NameLabelKey { get; private set; } = "useKey";
        [field: SerializeField] public string NameLabelNecklace { get; private set; } = "takeItem";
    }
}