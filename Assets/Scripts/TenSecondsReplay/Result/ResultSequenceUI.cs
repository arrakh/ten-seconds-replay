using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TenSecondsReplay.Result
{
    public class ResultSequenceUI : MonoBehaviour
    {
        [SerializeField] private GameObject holder;
        [SerializeField] private DeniedElement deniedPrefab;
        [SerializeField] private ApprovalElement approvalPrefab;
        [SerializeField] private RectTransform approvalArea;
        [SerializeField] private RectTransform deniedParent;
        [Header("Scale Animation")] 
        [SerializeField] private AnimationCurve scaleCurve;
        [SerializeField] private float scaleFrom;
        [SerializeField] private float scaleDuration;

        private Tween scale;
        
        private List<DeniedElement> spawnedDeniedElements = new();
        private List<ApprovalElement> spawnedApprovalElements = new();

        private int lastScore, lastFail;

        public void Initialize(int maxFail)
        {
            for (int i = 0; i < maxFail; i++)
            {
                var element = Instantiate(deniedPrefab, deniedParent, false);
                spawnedDeniedElements.Add(element);
            }
        }
        
        public void Display(int score, int failAmount)
        {
            holder.SetActive(true);

            StartEnterAnimation();

            if (score > lastScore) AddScore(score - lastScore);
            if (failAmount > lastFail) AddFail(failAmount - lastFail);

            lastFail = failAmount;
            lastScore = score;
        }

        public void Hide()
        {
            holder.SetActive(false);
        }

        private void AddFail(int failAmount)
        {
            for (int i = lastFail; i < lastFail + failAmount; i++)
            {
                var element = spawnedDeniedElements[i];
                element.StartAnimation();
            }
        }

        private void AddScore(int score)
        {
            for (int i = 0; i < score; i++)
            {
                var element = Instantiate(approvalPrefab, approvalArea, false);
                var rect = approvalArea.rect;

                var x = Random.Range(rect.xMin, rect.xMax);
                var y = Random.Range(rect.yMin, rect.yMax);

                element.RectTransform.anchoredPosition =  new Vector2(x, y);
                element.StartAnimation();
                spawnedApprovalElements.Add(element);
            }
        }

        private void StartEnterAnimation()
        {
            scale?.Kill();
            holder.transform.localScale = Vector3.one * scaleFrom;
            scale = holder.transform.DOScale(1f, scaleDuration).SetEase(scaleCurve);
        }
    }
}