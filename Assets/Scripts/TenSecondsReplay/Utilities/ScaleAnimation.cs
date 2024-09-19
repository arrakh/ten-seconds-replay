﻿using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

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

        [Header("Options")]
        [SerializeField] private bool randomizeDuration;
        [SerializeField] private float randomDurationRange;
        
        private Tween tween;
        
        private void Start()
        {
            if (playOnStart) StartAnimation();
        }

        public void StartAnimation()
        {
            StopAnimation();

            target.transform.localScale = from;
            
            var dur = randomizeDuration
                ? Random.Range(duration - randomDurationRange, duration + randomDurationRange)
                : duration;
            
            tween = target.DOScale(to, dur)
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