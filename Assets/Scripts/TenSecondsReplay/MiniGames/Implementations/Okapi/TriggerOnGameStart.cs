using OkapiKit;
using UnityEngine;

namespace TenSecondsReplay.MiniGames.Implementations.Okapi
{
    public class TriggerOnGameStart : Trigger
    {
        [SerializeField] private OkapiMiniGame miniGame;
        
        public override string GetTriggerTitle() => "On Minigame Start";

        private void OnEnable() => miniGame.onKeyInput += OnKeyInput;

        private void OnDisable() => miniGame.onKeyInput -= OnKeyInput;

        private void OnKeyInput()
        {
            if (!isTriggerEnabled) return;
            if (!EvaluatePreconditions()) return;

            ExecuteTrigger();
        }

        public override string GetRawDescription(string ident, GameObject refObject)
        {
            return "When prompt ends and Minigame starts";
        }

        protected override void CheckErrors()
        {
            base.CheckErrors();
            
            if (miniGame == null)
                _logs.Add(new LogEntry(LogEntry.Type.Error, "Okapi Minigame object is not yet assigned!"));
        }
    }
}