using UnityEngine;

namespace TenSecondsReplay.MiniGames
{
    public abstract class MiniGameObject : MonoBehaviour
    {
        public abstract string PromptText { get; }
        public bool HasWon { get; protected set; }
        public abstract void OnInput();
        public abstract void OnGameStart();
    }
}
