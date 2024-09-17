using UnityEngine;
using UnityEngine.UI;

namespace TenSecondsReplay.MiniGames.Implementations.Handshake
{
    public class HandshakeOption : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private string handshakeId;
        [SerializeField] private Color normalColor, highlightedColor;

        public string Id => handshakeId;
        
        public void SetHighlight(bool isHighlighted)
        {
            background.color = isHighlighted ? highlightedColor : normalColor;
        }
    }
}