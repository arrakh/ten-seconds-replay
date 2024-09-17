using System;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TenSecondsReplay.MiniGames.Implementations.Photo
{
    public class PhotoMiniGame : MiniGameObject
    {
        [SerializeField] private PhotoCameraObject cameraObject;
        [SerializeField] private PhotoFlashUI photoFlashUi;
        [SerializeField] private GameObject[] subjects;
        [SerializeField] private int startingSubjectCount = 4;
        [SerializeField] private TextMeshProUGUI debugText;
        
        public override string PromptText => "Take a Picture!";
        public override void OnInput()
        {
            cameraObject.Stop();
            photoFlashUi.Flash();
            HasWon = !cameraObject.HasSubject();

            debugText.text = HasWon ? "Nice pic!" : "YOU DIED";
        }

        private void Start()
        {
            var randomSubjects = subjects.ToList()
                .OrderBy(x => Random.Range(0, subjects.Length))
                .Take(startingSubjectCount);

            foreach (var subject in subjects)
                subject.SetActive(false);
            
            foreach (var subject in randomSubjects)
                subject.SetActive(true);
        }

        public override void OnGameStart()
        {
            cameraObject.StartMoving();
        }
    }
}