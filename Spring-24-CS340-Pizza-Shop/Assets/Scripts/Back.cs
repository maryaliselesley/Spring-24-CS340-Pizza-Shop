using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    // Array to store scene names
    private static List<string> sceneHistory = new List<string>();
   
    void Start()
    {
        // Initialize the scene history array with capacity for 10 scenes

        // Call a method to add the current scene to the array
        if (sceneHistory.Contains(SceneManager.GetActiveScene().name)) {
            return;
        
        }
       sceneHistory.Add(SceneManager.GetActiveScene().name);
        Debug.Log(string.Format("Here's the list: ({0}).", string.Join(", ", sceneHistory)));
    }

  
  

    // Method to go back to the previous scene
    public void GoBack()
    {
        // Check if there are scenes in history

        if (sceneHistory.Count > 0)
        {
            sceneHistory.RemoveAt( sceneHistory.Count-1);
            Debug.Log(" SceneHistory is going back to " + sceneHistory[sceneHistory.Count - 1] + " ");
            SceneManager.LoadScene(sceneHistory[sceneHistory.Count - 1]); 
      
        }
        else
        {
            Debug.LogWarning("No previous scene available.");
        }
    }
}