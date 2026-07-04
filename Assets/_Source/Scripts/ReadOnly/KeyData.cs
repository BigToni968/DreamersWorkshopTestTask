using UnityEngine;

namespace TestTask.ReadOnly
{
    [CreateAssetMenu(menuName = "Game/Config/Item/Key")]
    public class KeyData : ScriptableObject
    {
        [field: SerializeField] public float Offset { get; private set; } = 1f;
        [field: SerializeField] public float Duration { get; private set; } = 2f;
        [field: SerializeField] public string NameScript { get; private set; } = "altar";
        [field: SerializeField] public string NameLabel { get; private set; } = "start";
    }
}