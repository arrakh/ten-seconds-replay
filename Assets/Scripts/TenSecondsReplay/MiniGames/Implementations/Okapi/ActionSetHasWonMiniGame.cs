using OkapiKit;
using UnityEngine;

namespace TenSecondsReplay.MiniGames.Implementations.Okapi.Editor
{
    public class ActionSetHasWonMiniGame : Action
    {
        [SerializeField] private OkapiMiniGame minigame;
        [SerializeField] private bool hasWon;
        
        public override string GetRawDescription(string ident, GameObject refObject)
        {
            var result = hasWon ? "win" : "lose";
            return $"Mark Minigame to {result}";
        }

        public override void Execute()
        {
            if (!enableAction) return;
            if (!EvaluatePreconditions()) return;
            
            minigame.SetHasWon(hasWon);
        }
        
        protected override void CheckErrors()
        {
            base.CheckErrors();
            
            if (minigame == null)
                _logs.Add(new LogEntry(LogEntry.Type.Error, "Okapi Mini Game object is not yet assigned!"));
        }
    }
}