using TenSecondsReplay.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace TenSecondsReplay.MiniGames.Implementations.Photo
{
    public class PhotoSubject : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite angrySprite;
        [SerializeField] private ScaleAnimation scaleAnim, bobAnim;
        
        public void TriggerAngry()
        {
            spriteRenderer.sprite = angrySprite;
            bobAnim.StopAnimation();
            scaleAnim.StartAnimation();
        }
    }
}