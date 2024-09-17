using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TenSecondsReplay.MiniGames.Implementations.Photo
{
    public class PhotoCameraObject : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2d;
        [SerializeField] private Collider2D collider2d;
        [SerializeField] private float startingSpeed;
        [SerializeField] private float minimumDeviation = 0.2f;

        private HashSet<Collider2D> subjects = new();

        private bool isStopped = false;

        private Vector2 currentVelocity;

        public void StartMoving()
        {
            var randomVelocity = Random.insideUnitCircle.normalized;
            randomVelocity.x = ClampDeviation(randomVelocity.x);
            randomVelocity.y = ClampDeviation(randomVelocity.y);
            Debug.Log($"RANDOM VELOCITY {randomVelocity}");
            rigidbody2d.velocity = currentVelocity = randomVelocity * startingSpeed;
        }
        
        private float ClampDeviation(float value)
        {
            return Mathf.Abs(value) < minimumDeviation ? minimumDeviation * Mathf.Sign(value) : value;
        }

        public bool HasSubject() => subjects.Count > 0;

        public void Stop()
        {
            rigidbody2d.velocity = currentVelocity = Vector2.zero;
            isStopped = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("PhotoSubject")) subjects.Add(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            subjects.Remove(other);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (isStopped) return;
            if (!other.gameObject.tag.Equals("Bounds")) return;

            var contact = other.GetContact(0);
            var contactNormal = contact.normal;

            var reflectedVelocity = Vector2.Reflect(currentVelocity, contactNormal);
            
            //Debug.Log($"VELOCITY {currentVelocity}, CONTACT {contact}, NORMAL {contactNormal}, REFLECTED {reflectedVelocity}");

            rigidbody2d.velocity = currentVelocity = reflectedVelocity;
        }
    }
}
