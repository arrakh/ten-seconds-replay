using System;
using System.Linq;
using TenSecondsReplay.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace TenSecondsReplay.MiniGames.Implementations.Food
{
    public class FoodMiniGame : MiniGameObject
    {
        [SerializeField] private Spinner spinner;
        [SerializeField] private Image foodImage;
        [SerializeField] private CutleryElement[] cutleries;
        [SerializeField] private CutleryPrompt[] prompts;
        [SerializeField] private float spinSpeed = 10;
        [SerializeField] private AudioSource beltSound, ambienceSound;

        [SerializeField] private AudioClip correctAnswer, wrongAnswer;
        [SerializeField] private AudioSource clickSound;

        private CutleryPrompt currentPrompt;
        private float currentPosition = 0f;
        private bool shouldSpin = false;
        
        private void Start()
        {
            currentPrompt = prompts.GetRandom();
            Debug.Log($"Current Promt is {currentPrompt.type}");
            foodImage.sprite = currentPrompt.image;
            spinner.Setup(cutleries.Select(x => x.transform as RectTransform).ToList());
        }

        private CutleryElement lastHighlighted = null;
        private RectTransform lastHighlightedRect = null;

        private void Update()
        {
            if (!shouldSpin) return;

            currentPosition += Time.deltaTime * spinSpeed;
            spinner.SetTargetPosition(currentPosition);
            
            var cutleryCard = spinner.GetElementAtNormalizedPosition(0.5f);

            if (lastHighlightedRect && cutleryCard.rectTransform.Equals(lastHighlightedRect)) return;
            
            if (!cutleryCard.rectTransform.TryGetComponent(out CutleryElement cutlery))
                throw new Exception($"OBJECT {cutleryCard.rectTransform.name} IS NOT A CUTLERY");
            
            if (lastHighlighted != null) lastHighlighted.SetHighlight(false);

            cutlery.SetHighlight(true);
            lastHighlighted = cutlery;
            lastHighlightedRect = cutleryCard.rectTransform;
        }

        public override string PromptText => "Try your dish!";
        public override void OnInput()
        {
            beltSound.Stop();

            shouldSpin = false;
            var cutleryCard = spinner.GetElementAtNormalizedPosition(0.5f);

            if (!cutleryCard.rectTransform.TryGetComponent(out CutleryElement cutlery))
                throw new Exception($"OBJECT {cutleryCard.rectTransform.name} IS NOT A CUTLERY");
            
            Debug.Log($"Answered with {cutlery.Type}");

            clickSound.clip = HasWon ? correctAnswer : wrongAnswer;
            clickSound.Play();

            HasWon = cutlery.Type == currentPrompt.type;
        }

        public override void OnGameStart()
        {
            shouldSpin = true;
            beltSound.Play();
            ambienceSound.Play();
        }
    }
}