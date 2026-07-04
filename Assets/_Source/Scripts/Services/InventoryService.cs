using TestTask.UI;
using Naninovel;

namespace TestTask
{
    [InitializeAtRuntime]
    public sealed class InventoryService : IEngineService
    {
        private const string INVENTORY_PREFAB_NAME = "inventory";

        private ISpawnManager _spawn;
        private Inventory _inventory;
        
        public UniTask InitializeServiceAsync()
        {
            _spawn = Engine.GetService<ISpawnManager>();
            return UniTask.CompletedTask;
        }

        public void ResetService()
        {
        }

        public void DestroyService()
        {
            if (_inventory != null)
                _spawn.DestroySpawned(INVENTORY_PREFAB_NAME);
        }

        public void SetItem(string itemName)
        {
            if (_inventory == null)
            {
                var spawnedObject = _spawn.GetSpawned(INVENTORY_PREFAB_NAME);
                spawnedObject.GameObject.TryGetComponent(out _inventory);
            }
            
            _inventory.SetIcon(itemName);
        }
    }
}