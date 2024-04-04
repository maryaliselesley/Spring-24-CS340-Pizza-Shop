/*using UnityEngine;
using System.Collections.Generic;
// to test the back button, only start at the login page
public class Back : MonoBehaviour
    private List<string> PreviousScene = new List<string>();
private void Start()
{
    Back.Add(Application.loadedLevelName);
    GameObject.SetGameObjectsActive(false);

}

public void AddCurrentSceneToLoadedScenes()
{
    PreviousScene.Add(Application.loadedLevelName);

}

public void LoadPreviousScene()
{
    string previousScene = string.Empty;
    if (previousScene.Count > 1) {
    previousScene = [previousScene.Count -1]};
    previousScene.RemoveAt(previousScene.Count - 1);
    Application.LoadLevel(previousScene);
}
else
{ PreviousScene = PreviousScene[0]
        Application.LoadLevel(previousScene)
        gameObject.SetActive(false);
}
{
    //List<String> BackBM = new List <String>();

   // public void goBack()
   // {
     ///   Debug.Log("The method worked yippee");


   // }
}*/