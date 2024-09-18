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
            minigame.SetHasWon(hasWon);
        }
    }
}