using System.Collections.Generic;
using UnityEngine;

namespace TenSecondsReplay.MiniGames.Implementations.Photo
{
    public class PhotoCameraFrame : MonoBehaviour
    {
        private HashSet<PhotoSubject> subjects = new();
        
        public bool HasSubject() => subjects.Count > 0;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PhotoSubject subject)) subjects.Add(subject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out PhotoSubject subject)) subjects.Remove(subject);
        }

        public void TurnAllAngry()
        {
            foreach (var subject in subjects)
                subject.TriggerAngry();
        }
    }
}