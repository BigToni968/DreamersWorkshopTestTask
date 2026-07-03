using UnityEngine;

namespace TestTask.ReadOnly
{
    [CreateAssetMenu(menuName = "Game/Config/Navigation")]
    public class NavigationData : ScriptableObject
    {
        [SerializeField] private string[] location;

        public string GetNextLocation(int index)
        {
            return location[index];
        }

        public bool IsShowSide(string curent, out int index)
        {
            index = 0;
            var hasLocation = curent.GetHashCode();
            foreach (var nameLocation in location)
            {
                if (hasLocation == nameLocation.GetHashCode())
                    break;
                index++;
            }

            var curentIndex = index;
            index++;
            return curentIndex < location.Length - 1;
        }
    }
}