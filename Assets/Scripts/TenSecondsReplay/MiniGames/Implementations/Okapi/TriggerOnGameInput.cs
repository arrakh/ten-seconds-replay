using System;
using OkapiKit;
using UnityEngine;

namespace TenSecondsReplay.MiniGames.Implementations.Okapi
{
    public class TriggerOnGameInput : Trigger
    {
        [SerializeField] private OkapiMiniGame miniGame;

        private Variable gameTime;
        
        public override string GetTriggerTitle() => "On Minigame Input";

        private void OnEnable() => miniGame.onKeyInput += OnKeyInput;

        private void OnDisable() => miniGame.onKeyInput -= OnKeyInput;

        private void OnKeyInput()
        {
            if (!isTriggerEnabled) return;
            if (!EvaluatePreconditions()) return;
            
            Debug.Log("ON GAME INPUT");

            ExecuteTrigger();
        }

        public override string GetRawDescription(string ident, GameObject refObject)
        {
            return "When Minigame input is triggered";
        }

        protected override void CheckErrors()
        {
            base.CheckErrors();
            
            if (miniGame == null)
                _logs.Add(new LogEntry(LogEntry.Type.Error, "Okapi Mini Game object is not yet assigned!"));
        }
    }
}