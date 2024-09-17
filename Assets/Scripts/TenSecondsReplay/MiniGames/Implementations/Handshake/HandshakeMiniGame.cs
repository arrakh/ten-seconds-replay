using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private Transform optionParent;
        [SerializeField] private float pointerSpeed = 1f;
        [SerializeField] private int optionCount = 3;
        [SerializeField] private TextMeshProUGUI debugText;
        [SerializeField] private float sliderPadding = 0.05f;
        
        private float alphaTimer = 0f;
        private bool isRunning = true;
        private List<HandshakeOption> spawnedOptions = new();

        private void Start()
        {
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
            HasWon = GetSelectedOption().IsCorrect;
            debugText.text = HasWon ? "Henllo :3" : "YOU DIED";
        }

        public override void OnGameStart()
        {
            
        }
    }
}