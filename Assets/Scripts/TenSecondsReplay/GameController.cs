using System.Collections;
using TenSecondsReplay.MiniGames;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TenSecondsReplay
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private KeyCode actionKey;
        [SerializeField] private MiniGameObject[] gamePrefabs;
        [SerializeField] private PromptUI promptUi;
        [SerializeField] private GameTimerUI gameTimerUI;
        [SerializeField] private float promptTime, gameTime;

        private MiniGameObject currentMiniGame;

        private Coroutine gameCoroutine;

        private void Update()
        {
            if (Input.GetKeyDown(actionKey)) currentMiniGame.OnInput();
        }

        private void Start()
        {
            StartRandomMiniGame();
        }

        private void StartRandomMiniGame()
        {
            if (currentMiniGame != null) Destroy(currentMiniGame.gameObject);
            
            var randIndex = Random.Range(0, gamePrefabs.Length);
            var randGame = gamePrefabs[randIndex];

            currentMiniGame = Instantiate(randGame);

            if (gameCoroutine != null) StopCoroutine(gameCoroutine);
            gameCoroutine = StartCoroutine(StartGameCoroutine());
        }

        private IEnumerator StartGameCoroutine()
        {
            promptUi.Display(currentMiniGame);
            
            yield return new WaitForSeconds(promptTime);
            promptUi.Hide();
            
            gameTimerUI.Display(gameTime);
            yield return new WaitForSeconds(gameTime);
            
            StartRandomMiniGame();
        }
    }
}