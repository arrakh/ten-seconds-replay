using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TenSecondsReplay
{
    public class GameTimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Image timerImage;
        //[SerializeField] private GameObject holder;

        private float currentTimer, maxTimer;
        
        public void Display(float time)
        {
            //holder.gameObject.SetActive(true);
            currentTimer = time;
            maxTimer = time;
        }

        public void Hide()
        {
            //holder.gameObject.SetActive(false);
        }

        private void Update()
        {
            currentTimer -= Time.deltaTime;
            currentTimer = Mathf.Clamp(currentTimer, 0f, float.MaxValue);

            if (currentTimer > 3f) timerText.text = $"{currentTimer:F0}";
            else timerText.text = $"{currentTimer:F1}";

            //timerImage.fillAmount = currentTimer / maxTimer;
        }
    }
}