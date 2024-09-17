using System;
using System.Linq;
using UnityEngine;

namespace TenSecondsReplay.MiniGames.Implementations.Food
{
    public class FoodMiniGame : MiniGameObject
    {
        [SerializeField] private Spinner spinner;
        [SerializeField] private CutleryElement[] cutleries;
        [SerializeField] private float spinSpeed = 10;

        private float currentPosition = 0f;
        private bool shouldSpin = false;
        
        private void Start()
        {
            spinner.Setup(cutleries.Select(x => x.transform as RectTransform).ToList());
        }
        
        

        private void Update()
        {
            if (!shouldSpin) return;

            currentPosition += Time.deltaTime * spinSpeed;
            spinner.SetTargetPosition(currentPosition);
        }

        public override string PromptText => "Try your dish!";
        public override void OnInput()
        {
            shouldSpin = false;
        }

        public override void OnGameStart()
        {
            shouldSpin = true;
        }
    }
}