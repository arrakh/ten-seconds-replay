using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TenSecondsReplay.Result
{
    public class DeniedElement : MonoBehaviour
    {
        [SerializeField] private GameObject filledGroup;
        [SerializeField] private Image image;
        [SerializeField] private ParticleSystem landParticle;

        [Header("Twist Animation")]
        [SerializeField] private float twistDuration = 0.4f;
        [SerializeField] private float twistScaleFrom = 1.2f;
        [SerializeField] private AnimationCurve twistScaleCurve;
        [SerializeField] private Vector2 twistRotateFromRange = new Vector2(-100f, -60f);
        [SerializeField] private Vector2 twistRotateToRange = new Vector2(-5f, 15f);
        [SerializeField] private AnimationCurve twistRotateCurve;

        [Header("Shake Animation")] 
        [SerializeField] private float shakeDuration = 1.2f;
        [SerializeField] private float shakeStrength = 10f;
        
        private Tween twistRotate, twistOpacity, twistScale, shake;

        public void StartAnimation()
        {
            filledGroup.gameObject.SetActive(true);
            
            image.color = Color.clear;
            twistOpacity?.Kill();
            twistOpacity = image.DOColor(Color.white, twistDuration);

            twistScale?.Kill();
            image.transform.localScale = Vector3.one * twistScaleFrom;
            twistScale = image.transform.DOScale(Vector3.one, twistDuration).SetEase(twistScaleCurve);
            
            twistRotate?.Kill();
            var randRotFrom = Random.Range(twistRotateFromRange.x, twistRotateFromRange.y);
            var randRotTo = Random.Range(twistRotateToRange.x, twistRotateToRange.y);
            image.transform.localEulerAngles = new Vector3(0, 0, randRotFrom);
            twistRotate = image.transform
                .DOLocalRotate(new Vector3(0, 0, randRotTo), twistDuration)
                .SetEase(twistRotateCurve)
                .OnComplete(LandAnimation);
        }

        private void LandAnimation()
        {
            landParticle.Stop();
            landParticle.Play();
            
            shake?.Kill();
            shake = transform.DOShakePosition(shakeDuration, new Vector3(shakeStrength, 0f));
        }
    }
}