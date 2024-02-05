using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

// to test the back button, only start at the login page
public class Back : MonoBehaviour
{
    // there should only be one instance of the list
    public static List<string> sceneHistory = new List<string>();

    // keep Debug.Log statements
    private void Start()
    {
        // add current scene to list if it's not already in the list
        string currentScene = SceneManager.GetActiveScene().name;
        if (!sceneHistory.Contains(currentScene))
        {
            sceneHistory.Add(currentScene);
        }
        Debug.Log("Scene History: " + string.Join(", ", sceneHistory));
    }

    public void GoBack()
    {
        // if there exist more than just the current scene
        if (sceneHistory.Count > 1)
        {
            sceneHistory.RemoveAt(sceneHistory.Count - 1); // remove current scene
            string previousScene = sceneHistory.ElementAt(sceneHistory.Count - 1); // get previous scene name
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("No previous scene available.");
        }
    }

    // for other scripts to reference
    public static void RemoveCurrentScene()
    {
        sceneHistory.RemoveAt(sceneHistory.Count - 1);
    }
}