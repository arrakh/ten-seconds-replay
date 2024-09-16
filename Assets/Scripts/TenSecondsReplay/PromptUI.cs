using OkapiKit;
using TenSecondsReplay.MiniGames;
using TMPro;
using UnityEngine;

namespace TenSecondsReplay
{
    public class PromptUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI promptText;
        [SerializeField] private AudioClip promptAudio;
        [SerializeField] private GameObject holder;
        
        public void Display(MiniGameObject miniGame)
        {
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