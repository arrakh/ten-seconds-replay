using System;
using UnityEngine;

namespace TenSecondsReplay.MiniGames.Implementations.Handshake
{
    [Serializable]
    public struct HandshakePrompt
    {
        public string id;
        public Sprite personSprite;
        public Sprite bgSprite;
    }
}