using System;
using UnityEngine;
using UnityEngine.UI;

namespace TenSecondsReplay
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] private Button menuButton, quitButton;
        [SerializeField] private GameObject holder;
        
        private bool isPaused;
        
        private void Awake()
        {
            menuButton.onClick.AddListener(OnMenuButton);
            quitButton.onClick.AddListener(OnQuitButton);
        }

        private void OnQuitButton()
        {
            Application.Quit();
        }

        void DeterminePaused()
        {
            holder.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;
                DeterminePaused();
            }
        }

        private void OnBackButton()
        {
            isPaused = false;
            DeterminePaused();
        }

        private void OnMenuButton()
        {
            
        }
    }
}