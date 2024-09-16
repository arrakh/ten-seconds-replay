using UnityEngine;
using UnityEngine.UI;

namespace MiniGames.Implementations.Handshake
{
    public class HandshakeOption : MonoBehaviour
    {
        [SerializeField] private Image background;
        
        public void SetHighlight(bool isHighlighted)
        {
            background.color = isHighlighted ? Color.white : Color.gray;
        }
    }
}