using UnityEngine;
using Naninovel;

namespace TestTask.Commands
{
    [CommandAlias("minigame")]
    public class MiniGameCommand : Command
    {
        [ParameterAlias(""),RequiredParameter]
        public StringParameter State;
        
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var state = State.Value.ToLower();
            
            if (state.GetHashCode() == "start".GetHashCode())
                    Engine.GetService<MiniGameService>().Show();
            else if (state.GetHashCode() == "finish".GetHashCode())
                    Engine.GetService<MiniGameService>().Hide();
            else
                    Debug.LogError("minigame ошибка параметра");
            
            return UniTask.CompletedTask;
        }
    }
}