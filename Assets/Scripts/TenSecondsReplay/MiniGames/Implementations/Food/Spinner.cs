using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TenSecondsReplay.MiniGames.Implementations.Food
{
    [ExecuteInEditMode]
    public class Spinner : MonoBehaviour
    {
        [Serializable]
        public struct Card
        {
            public RectTransform rectTransform;
            public float offset;

            public Card(RectTransform rectTransform, float offset)
            {
                this.rectTransform = rectTransform;
                this.offset = offset;
            }
        }

        public Action onCheckpoint;
        
        [SerializeField] float padding = 50f;
        [SerializeField] float elementWidth = 700f;
        [SerializeField] Vector2 elementHeightRange = new (1000f, 1200f);
        [SerializeField] AnimationCurve scaleCurve;
        [SerializeField] float targetPosition = 0f;

        RectTransform thisRect;
        Tween spinTween;
        float currentTargetOffset, currentTargetMiddleOffset;
        List<Card> cards = new List<Card>();
        float checkPoint = 0;

        void Awake()
        {
            thisRect = GetComponent<RectTransform>();
        }

        void Update()
        {
            foreach (RectTransform child in transform)
            {
                var alpha = GetDistanceAlpha(child);
            
                var ySize = Mathf.Lerp(elementHeightRange.x, elementHeightRange.y, scaleCurve.Evaluate(1 - alpha));
                child.sizeDelta = new Vector2(elementWidth, ySize);
            }
            
            SetPosition(targetPosition);
            if (targetPosition < checkPoint)
            {
                checkPoint -= elementWidth + padding;
                OnCheckpointPassed();
            }
        }

        void OnCheckpointPassed()
        {
            onCheckpoint?.Invoke();
        }

        public void Setup(List<RectTransform> objects)
        {
            Stop();

            cards.Clear();
            for (var i = 0; i < objects.Count; i++)
            {
                var offset = ((elementWidth + padding) * i);
                cards.Add(new Card(objects[i], offset));
            }
        }

        public void SetPosition(RectTransform target)
        {
            var totalWidth = (elementWidth + padding) * cards.Count;
            foreach (var card in cards)
            {
                if (card.rectTransform != target) continue;
                targetPosition = GetPosition(card.offset, totalWidth);
            }
        }

        float GetPosition(float offset, float totalWidth)
        {
            var halfFullWidth = totalWidth / 2f;
            var halfElement = elementWidth / 2f;
            var pos = halfFullWidth - offset - halfElement;
            if (pos < 0) pos += totalWidth;

            return pos;
        }

        public void SetTargetPosition(float position) => targetPosition = position; 

        public void Stop()
        {
            targetPosition = currentTargetOffset;
            //Debug.Log($"SETTING TARGET POSITION TO {targetPosition}");
        }

        public void SetPosition(float pixelPosition)
        {
            foreach (var card in cards)
            {
                var pos = card.rectTransform.anchoredPosition;
                var totalWidth = (elementWidth + padding) * cards.Count;
                pos.x = (card.offset + pixelPosition) % totalWidth - totalWidth / 2f + elementWidth / 2f;
                card.rectTransform.anchoredPosition = pos;
            }
        }

        float GetDistanceAlpha(RectTransform child)
        {
            var halfThisRect = thisRect.rect.width / 2f;
            var pos = child.anchoredPosition;
            var dist = Vector3.Distance(pos, thisRect.anchoredPosition);
            var halfWidth = elementWidth / 2f;
            return dist / (halfThisRect + halfWidth);
        }

#if UNITY_EDITOR

        [UnityEngine.ContextMenu("Set Layout")]
        void SetLayout()
        {
            cards.Clear();
            for (var i = 0; i < transform.childCount; i++)
            {
                var obj = transform.GetChild(i);
                if (!obj.TryGetComponent(out RectTransform rect))
                    throw new Exception("Object must have a RectTransform!");
                
                var offset = ((elementWidth + padding) * i);
                cards.Add(new Card(rect, offset));
            }
        }
#endif
    }
}