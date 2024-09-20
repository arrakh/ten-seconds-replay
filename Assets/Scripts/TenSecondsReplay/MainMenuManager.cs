using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
   
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCutscene();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    void StartCutscene()
    {
        SceneManager.LoadScene("CutScene");
    }

    void QuitGame()
    {

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}