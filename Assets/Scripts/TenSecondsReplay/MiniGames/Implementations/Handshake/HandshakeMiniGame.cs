using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TenSecondsReplay.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TenSecondsReplay.MiniGames.Implementations.Handshake
{
    public class HandshakeMiniGame : MiniGameObject
    {
        [SerializeField] private Slider pointer;
        [SerializeField] private HandshakeOption[] possibleOptionsPrefab;
        [SerializeField] private HandshakePrompt[] prompts;
        [SerializeField] private Transform optionParent;
        [SerializeField] private float pointerSpeed = 1f;
        [SerializeField] private int optionCount = 3;
        [SerializeField] private float sliderPadding = 0.05f;
        [SerializeField] private Image person, bg;
        [SerializeField] private Transform arrowTransform;
        [SerializeField] private ParticleSystem successParticle, failParticle;
        [SerializeField] private ScaleAnimation answerAnim, bobAnim;
        
        private float alphaTimer = 0f;
        private bool isRunning = true;
        private List<HandshakeOption> spawnedOptions = new();
        private HandshakePrompt currentPrompt;

        private void Start()
        {
            currentPrompt = prompts.GetRandom();
            person.sprite = currentPrompt.personSprite;
            bg.sprite = currentPrompt.bgSprite;
            SpawnOptions();
        }

        private void SpawnOptions()
        {
            var prefabs = possibleOptionsPrefab.ToList()
                .OrderBy(x => Random.Range(0, possibleOptionsPrefab.Length))
                .Take(optionCount);

            foreach (var prefab in prefabs)
            {
                var option = Instantiate(prefab, optionParent);
                spawnedOptions.Add(option);
            }

            if (currentPrompt.id != "raghad") return;
            var rnd = spawnedOptions.GetRandom();
            rnd.SetId("raghad");
            rnd.SetText("Greet from afar");
        }

        private void Update()
        {
            if (!isRunning) return; 
            UpdateSlider();
            UpdateSelection();
        }

        private void UpdateSelection()
        {
            var selection = GetSelectedOption();

            foreach (var option in spawnedOptions)
                option.SetHighlight(selection.Equals(option));
        }

        HandshakeOption GetSelectedOption()
        {
            var segmentSize = 1.0f / optionCount;

            int option = (int)(pointer.value / segmentSize);

            if (option == optionCount) option = optionCount - 1;

            return spawnedOptions[option];
        }

        private void UpdateSlider()
        {
            alphaTimer += Time.deltaTime * pointerSpeed;
            var alpha = Mathf.InverseLerp(-1, 1, Mathf.Sin(alphaTimer));
            pointer.value = Mathf.Lerp(sliderPadding, 1f - sliderPadding, alpha);
        }

        public override string PromptText => "Greet the locals!";

        public override void OnInput()
        {   
            isRunning = false;
            HasWon = GetSelectedOption().Id.Equals(currentPrompt.id);
            answerAnim.StartAnimation();
            bobAnim.StopAnimation();
            
            arrowTransform.transform.localScale = Vector3.one * 1.2f;
            arrowTransform.DOScale(1f, 0.25f).SetEase(Ease.OutCirc);

            var particle = HasWon ? successParticle : failParticle;
            particle.Stop();
            particle.Play();

            person.sprite = HasWon ? currentPrompt.happySprite : currentPrompt.angrySprite;
        }

        public override void OnGameStart()
        {
            
        }
    }
}