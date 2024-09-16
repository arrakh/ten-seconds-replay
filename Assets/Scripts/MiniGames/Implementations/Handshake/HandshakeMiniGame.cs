using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MiniGames.Implementations.Handshake
{
    public class HandshakeMiniGame : MiniGameObject
    {
        [SerializeField] private Slider pointer;
        [SerializeField] private HandshakeOption[] possibleOptionsPrefab;
        [SerializeField] private Transform optionParent;
        [SerializeField] private float pointerSpeed = 1f;
        [SerializeField] private int optionCount = 3;
        
        private float alphaTimer = 0f;
        private bool isRunning = true;
        private List<HandshakeOption> spawnedOptions = new();

        private void Start()
        {
            SpawnOptions();
        }

        private void SpawnOptions()
        {

            for (int i = 0; i < optionCount; i++)
            {
                var randCount = Random.Range(0, possibleOptionsPrefab.Length);

                var randPrefab = possibleOptionsPrefab[randCount];

                var option = Instantiate(randPrefab, optionParent).GetComponent<HandshakeOption>();
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
            var selection = GetOption();

            foreach (var option in spawnedOptions)
                option.SetHighlight(selection.Equals(option));
        }

        HandshakeOption GetOption()
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
            pointer.value = alpha;
        }

        public override void OnInput()
        {
            isRunning = false;
        }
    }
}