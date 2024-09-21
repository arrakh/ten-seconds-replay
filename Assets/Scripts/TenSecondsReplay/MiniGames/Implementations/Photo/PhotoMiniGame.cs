using System;
using System.Linq;
using TenSecondsReplay.Utilities;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TenSecondsReplay.MiniGames.Implementations.Photo
{
    public class PhotoMiniGame : MiniGameObject
    {
        [SerializeField] private PhotoFlashUI photoFlashUi;
        [SerializeField] private PhotoSubject[] subjects;
        [SerializeField] private PhotoCameraObject[] cameras;
        [SerializeField] private Sprite[] backgrounds;
        [SerializeField] private SpriteRenderer backgroundRenderer;
        [SerializeField] private int startingSubjectCount = 4;
        [SerializeField] private AudioSource ambienceSound;

        private PhotoCameraObject cameraObject;
        
        public override string PromptText => "Take a Picture!";
        public override void OnInput()
        {
            cameraObject.Stop();
            photoFlashUi.Flash();
            HasWon = !cameraObject.HasSubject();
            cameraObject.TurnAllAngry();
        }

        private void Start()
        {
            backgroundRenderer.sprite = backgrounds.GetRandom();
            
            var randomSubjects = subjects.ToList()
                .OrderBy(_ => Random.Range(0, subjects.Length))
                .Take(startingSubjectCount);

            foreach (var subject in subjects)
                subject.gameObject.SetActive(false);
            
            foreach (var subject in randomSubjects)
                subject.gameObject.SetActive(true);
        }

        public override void OnGameStart()
        {
            ambienceSound.Play();
            cameraObject = cameras.GetRandom();

            foreach (var cam in cameras)
                cam.gameObject.SetActive(cameraObject.Equals(cam));
            
            cameraObject.StartMoving();

        }
    }
}