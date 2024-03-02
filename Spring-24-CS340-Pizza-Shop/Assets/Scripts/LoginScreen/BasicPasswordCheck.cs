using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BasicPasswordCheck : MonoBehaviour
{

    string usernameText=" ";
    string passwordText=" ";

    [SerializeField]
    private GameObject warningMessage;

    [SerializeField]
    private TMP_InputField input;

    [SerializeField]
    private string correctUsername = "Username";

    [SerializeField]
    private string correctPassword = "Password";


    [SerializeField]
    private string sceneName;


    public void CheckPassword() {

        if (passwordText == correctPassword && usernameText == correctUsername) {
            warningMessage.SetActive(false);
            SceneManager.LoadScene(sceneName);
        }

        else { 
            if (warningMessage != null) {warningMessage.SetActive(true);} 
        }
    }

    public void ImportUsernameField(string text) {usernameText=text; Debug.Log(usernameText);}

    public void ImportPasswordField(string text) {passwordText=text; Debug.Log(passwordText);}


}
