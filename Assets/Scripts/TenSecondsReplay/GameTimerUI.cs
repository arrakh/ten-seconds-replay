using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TenSecondsReplay
{
    public class GameTimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        private void Update()
        {
            var currentTimer = GameController.CurrentTimer;
            
            if (currentTimer > 3f) timerText.text = $"{currentTimer:F0}";
            else timerText.text = $"{currentTimer:F1}";
        }
    }
}