using System;
using MiniGames;
using UnityEngine;

namespace DefaultNamespace
{
    public class MiniGameController : MonoBehaviour
    {
        [SerializeField] private KeyCode actionKey;
        [SerializeField] private MiniGameObject currentMiniGame; //should be spawned dynamically

        private void Update()
        {
            if (Input.GetKeyDown(actionKey)) currentMiniGame.OnInput();
        }
    }
}