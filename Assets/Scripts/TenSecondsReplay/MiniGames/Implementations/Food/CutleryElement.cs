using UnityEngine;
using UnityEngine.UI;

namespace TenSecondsReplay.MiniGames.Implementations.Food
{
    public class CutleryElement : MonoBehaviour
    {
        [SerializeField] private CutleryType type;
        [SerializeField] private Image background;

        public CutleryType Type => type;

        public void SetHighlight(bool isHighlighted)
        {
            //background.color = isHighlighted ? Color.white : Color.gray;
        }
    }
}