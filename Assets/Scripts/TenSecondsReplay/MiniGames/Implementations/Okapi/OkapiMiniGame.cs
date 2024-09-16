using System;
using UnityEngine;

namespace TenSecondsReplay.MiniGames.Implementations.Okapi
{
    public class OkapiMiniGame : MiniGameObject
    {
        [SerializeField] private string promptText;

        public override string PromptText => promptText;

        public event Action onKeyInput;
        public event Action onGameStart;
        
        public override void OnInput()
        {
            onKeyInput?.Invoke();
        }

        public override void OnGameStart()
        {
            onGameStart?.Invoke();
        }
    }
}