using System.Collections;
using TenSecondsReplay.MiniGames;
using TenSecondsReplay.Result;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace TenSecondsReplay
{
    public class GameController : MonoBehaviour
    {
        public static float CurrentTimer => _currentTimer;
        public static float CurrentMaxTimer => _currentMaxTimer;
        private static float _currentTimer;
        private static float _currentMaxTimer;
        
        [SerializeField] private KeyCode actionKey;
        [SerializeField] private MiniGameObject[] gamePrefabs;
        [SerializeField] private PromptSequenceUI promptSequenceUI;
        [SerializeField] private ResultSequenceUI resultSequenceUI;
        [SerializeField] private GameTimerUI gameTimerUI;
        [SerializeField] private GameOverUI gameOverUI;
        [SerializeField] private float promptTime, gameTime, resultTime;
        [SerializeField] private int maxFail = 3;
        
        [Header("DEBUG")] 
        [SerializeField] private bool useDebugMiniGame;
        [SerializeField] private int debugMiniGameIndex;

        public GameState State => state;

        private int fail, score;
        private float difficultyValue = 1f;
        private GameState state;

        private int lastRandomIndex = int.MaxValue;

        private MiniGameObject currentMiniGame;

        private Coroutine gameCoroutine;

        private void Update()
        {
            if (state != GameState.Game) return;
            
            if (Input.GetKeyDown(actionKey) || Input.GetMouseButtonDown(0)) 
                currentMiniGame.OnInput();
            
            _currentTimer -= Time.deltaTime;
            _currentTimer = Mathf.Clamp(_currentTimer, 0f, float.MaxValue);
        }

        private void Start()
        {
            resultSequenceUI.Initialize(maxFail);
            StartRandomMiniGame();
        }

        private void StartRandomMiniGame()
        {
            if (currentMiniGame != null) Destroy(currentMiniGame.gameObject);

            int randIndex;
            
            do
            {
                randIndex = Random.Range(0, gamePrefabs.Length);
            } while (gamePrefabs.Length != 1 && randIndex == lastRandomIndex);
                
            var randGame = gamePrefabs[randIndex];
            lastRandomIndex = randIndex;

            
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
            promptSequenceUI.Display(currentMiniGame);
            
            yield return new WaitForSeconds(promptTime);
            promptSequenceUI.Hide();
            
            state = GameState.Game;
            currentMiniGame.OnGameStart();
            _currentTimer = _currentMaxTimer = gameTime;
            yield return new WaitForSeconds(gameTime);

            state = GameState.Result;

            EvaluateGame();
            
            resultSequenceUI.Display(score, fail);
            yield return new WaitForSeconds(resultTime);

            if (fail < maxFail) StartRandomMiniGame();
            else
            {
                state = GameState.GameOver;
                gameOverUI.Display(score);
            }
            
            resultSequenceUI.Hide();
        }

        private void EvaluateGame()
        {
            var success = currentMiniGame.HasWon;

            if (success) score++;
            else fail++;
        }
    }
}