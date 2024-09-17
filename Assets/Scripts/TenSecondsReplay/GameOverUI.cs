using UnityEngine;

namespace TenSecondsReplay
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private GameObject holder;
        
        public void Display(int score)
        {
            holder.SetActive(true);
        }

        public void Hide()
        {
            holder.SetActive(false);
        }
    }
}