using System;
using DG.Tweening;
using UnityEngine;

namespace TenSecondsReplay.Utilities
{
    public class ScaleAnimation : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool playOnStart = true;
        [SerializeField] private Vector3 from, to;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;
        [SerializeField] private int loopCount;
        [SerializeField] private LoopType loopType;

        private Tween tween;
        
        private void Start()
        {
            if (playOnStart) StartAnimation();
        }

        public void StartAnimation()
        {
            StopAnimation();

            target.transform.localScale = from;
            tween = target.DOScale(to, duration)
                .SetEase(ease)
                .SetLoops(loopCount, loopType);
        }

        public void StopAnimation()
        {
            Debug.Log("STOPPING SCALE");
            if (tween != null) tween.Kill();
        }
    }
}