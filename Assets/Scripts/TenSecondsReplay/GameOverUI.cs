using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TenSecondsReplay
{
    public class GameOverUI : MonoBehaviour
    {
        private const string HIGHSCORE_KEY = "highscore";
        
        [SerializeField] private GameObject holder;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private GameController gameController;
        
        public void Display(int score)
        {
            var highScore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
            if (score > highScore) PlayerPrefs.SetInt(HIGHSCORE_KEY, score);
            
            scoreText.text = score.ToString();
            highScoreText.text = score > highScore ? score.ToString() : highScore.ToString();
            
            holder.SetActive(true);
        }

        private void Update()
        {
            if (gameController.State != GameState.GameOver) return;
            
            if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("Main");
            if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Menu");
        }
    }
}