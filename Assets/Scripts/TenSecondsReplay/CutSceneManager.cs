using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{
    public List<GameObject> cutsceneObjects; 

    private int currentObjectIndex = 0;

    void Start()
    {
        // Ensure all objects are hidden except the first one
        for (int i = 0; i < cutsceneObjects.Count; i++)
        {
            cutsceneObjects[i].SetActive(i == 0); // Only activate the first object
        }
    }

    void Update()
    {
        // Check if the user presses the space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextObject();
        }
    }

    void DisplayNextObject()
    {
        // Hide the current object
        cutsceneObjects[currentObjectIndex].SetActive(false);

        // Move to the next object
        currentObjectIndex++;

        // If there are more objects to show, display the next one
        if (currentObjectIndex < cutsceneObjects.Count)
        {
            cutsceneObjects[currentObjectIndex].SetActive(true);
        }
        else
        {
            // If we've reached the end, load the main game scene
            LoadMainGameScene();
        }
    }

    void LoadMainGameScene()
    {
        SceneManager.LoadScene("Main");
        Debug.Log("play main scene");
    }
}
