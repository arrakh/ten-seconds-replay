using OkapiKit;
using TenSecondsReplay.MiniGames;
using TMPro;
using UnityEngine;

namespace TenSecondsReplay
{
    public class PromptSequenceUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI promptText;
        [SerializeField] private AudioClip promptAudio;
        [SerializeField] private GameObject holder;
        [SerializeField] private TextMeshProUGUI healthText, scoreText;
        
        public void Display(int score, int health, MiniGameObject miniGame)
        {
            scoreText.text = score.ToString();
            healthText.text = health.ToString();
            
            holder.SetActive(true);
            
            promptText.text = miniGame.PromptText;
            var instance = SoundManager.instance;
            Debug.Log(instance == null);
            SoundManager.PlaySound(promptAudio);
        }

        public void Hide()
        {
            holder.SetActive(false);
        }
    }
}