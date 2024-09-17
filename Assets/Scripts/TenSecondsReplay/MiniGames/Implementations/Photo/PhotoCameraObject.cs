using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TenSecondsReplay.MiniGames.Implementations.Photo
{
    public class PhotoCameraObject : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2d;
        [SerializeField] private float startingSpeed;

        private void Start()
        {
            var randomVelocity = Random.insideUnitCircle.normalized * startingSpeed;
            rigidbody2d.velocity = randomVelocity;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.tag.Equals("Bounds")) return;
            var difference = other.transform.position - transform.position;
            Debug.Log(difference.normalized);
            //var reflectedVelocity = rigidbody2d.velocity
        }
    }
}
