using TMPro;
using UnityEngine;

namespace TenSecondsReplay
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private GameObject holder;
        [SerializeField] private TextMeshProUGUI scoreText;
        
        public void Display(int score)
        {
            scoreText.text = score.ToString();
            holder.SetActive(true);
        }

        public void Hide()
        {
            holder.SetActive(false);
        }
    }
}