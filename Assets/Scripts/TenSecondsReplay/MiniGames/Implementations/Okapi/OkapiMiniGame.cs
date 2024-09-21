using System;
using OkapiKit;
using UnityEngine;
using Action = System.Action;

namespace TenSecondsReplay.MiniGames.Implementations.Okapi
{
    public class OkapiMiniGame : MiniGameObject
    {
        [SerializeField] private string promptText;

        private Variable gameTime;
        private Variable passedGameTime;

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

        public void SetHasWon(bool won) => HasWon = won;

        private void Update()
        {
            gameTime.SetValue(GameController.CurrentTimer);
            passedGameTime.SetValue(GameController.CurrentMaxTimer - GameController.CurrentTimer);
        }

        private void Awake()
        {
            gameTime = Resources.Load<Variable>("MiniGameTime");
            passedGameTime = Resources.Load<Variable>("PassedMiniGameTime");
        }
    }
}