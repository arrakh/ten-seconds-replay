using OkapiKit;
using UnityEngine;
using UnityEngine.UI;

namespace TenSecondsReplay.MiniGames.Implementations.Photo
{
    public class PhotoFlashUI : MonoBehaviour
    {
        [SerializeField] private Image whiteImage;
        [SerializeField] private AudioClip flashAudio;
        [SerializeField] private float fromAlpha, duration;
        
        public void Flash()
        {
            whiteImage.color = new Color(1f, 1f, 1f, fromAlpha);
            whiteImage.CrossFadeAlpha(0f, duration, false);
            SoundManager.PlaySound(flashAudio);
        }
    }
}