using Naninovel;

namespace TestTask
{
    [CommandAlias("inv")]
    public class InventoryCommand : Command
    {
        [ParameterAlias("Item"),RequiredParameter]
        public StringParameter Item;
        
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var item = Item.Value.ToLower();
            Engine.GetService<InventoryService>().SetItem(item);
            return UniTask.CompletedTask;
        }
    }
}