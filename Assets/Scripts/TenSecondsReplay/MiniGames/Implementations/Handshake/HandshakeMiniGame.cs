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
        [SerializeField] private TextMeshProUGUI debugText;
        [SerializeField] private float sliderPadding = 0.05f;
        [SerializeField] private Image person, bg;
        [SerializeField] private Transform arrowTransform;
        
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

        public override string PromptText => "Shake Hands!";

        public override void OnInput()
        {
            isRunning = false;
            HasWon = GetSelectedOption().Id.Equals(currentPrompt.id);
            debugText.text = HasWon ? "Henllo :3" : "YOU DIED";
            
            arrowTransform.transform.localScale = Vector3.one * 1.2f;
            arrowTransform.DOScale(1f, 0.25f).SetEase(Ease.OutCirc);
        }

        public override void OnGameStart()
        {
            
        }
    }
}