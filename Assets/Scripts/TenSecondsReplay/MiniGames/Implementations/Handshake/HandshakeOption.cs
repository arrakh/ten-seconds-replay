using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TenSecondsReplay.MiniGames.Implementations.Handshake
{
    public class HandshakeOption : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI optionText;
        [SerializeField] private Image background;
        [SerializeField] private string handshakeId;
        [SerializeField] private Color normalColor, highlightedColor;

        public string Id => handshakeId;

        public void SetId(string newId) => handshakeId = newId;
        
        public void SetHighlight(bool isHighlighted)
        {
            background.color = isHighlighted ? highlightedColor : normalColor;
        }

        public void SetText(string text) => optionText.text = text;
    }
}