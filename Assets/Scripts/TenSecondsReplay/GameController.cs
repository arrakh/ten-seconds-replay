using System.Collections;
using TenSecondsReplay.MiniGames;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace TenSecondsReplay
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private KeyCode actionKey;
        [SerializeField] private MiniGameObject[] gamePrefabs;
        [FormerlySerializedAs("promptUi")] [SerializeField] private PromptSequenceUI promptSequenceUI;
        [SerializeField] private GameTimerUI gameTimerUI;
        [SerializeField] private GameOverUI gameOverUI;
        [SerializeField] private float promptTime, gameTime;
        [SerializeField] private int maxHealth = 3;
        
        [Header("DEBUG")] 
        [SerializeField] private bool useDebugMiniGame;
        [SerializeField] private int debugMiniGameIndex;

        private int health, score;
        private float difficultyValue = 1f;
        private GameState state;

        private MiniGameObject currentMiniGame;

        private Coroutine gameCoroutine;

        private void Update()
        {
            if (state != GameState.Game) return;
            
            if (Input.GetKeyDown(actionKey) || Input.GetMouseButtonDown(0)) 
                currentMiniGame.OnInput();
        }

        private void Start()
        {
            health = maxHealth;
            StartRandomMiniGame();
        }

        private void StartRandomMiniGame()
        {
            if (currentMiniGame != null) Destroy(currentMiniGame.gameObject);
            
            var randIndex = Random.Range(0, gamePrefabs.Length);
            var randGame = gamePrefabs[randIndex];
            
            #if UNITY_EDITOR
            if (useDebugMiniGame) randGame = gamePrefabs[debugMiniGameIndex];
            #endif

            currentMiniGame = Instantiate(randGame);

            if (gameCoroutine != null) StopCoroutine(gameCoroutine);
            gameCoroutine = StartCoroutine(StartGameCoroutine());
        }

        private IEnumerator StartGameCoroutine()
        {
            state = GameState.Prompt;
            promptSequenceUI.Display(score, health, currentMiniGame);
            
            yield return new WaitForSeconds(promptTime);
            promptSequenceUI.Hide();
            
            state = GameState.Game;
            currentMiniGame.OnGameStart();
            gameTimerUI.Display(gameTime);
            yield return new WaitForSeconds(gameTime);

            EvaluateGame();
            
            if (health > 0) StartRandomMiniGame();
            else gameOverUI.Display(score);
        }

        private void EvaluateGame()
        {
            var success = currentMiniGame.HasWon;

            if (success) score++;
            else health--;
        }
    }
}