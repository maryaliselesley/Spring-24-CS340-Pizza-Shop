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
    private string employeeUsername = "Username";

    [SerializeField]
    private string employeePassword = "Password";

    [SerializeField]
    private string managerUsername = "Username";

    [SerializeField]
    private string managerPassword = "Password";



    [SerializeField]
    private string employeeScene;
    
    [SerializeField]
    private string managerScene;


    public void CheckPassword() {

        if (passwordText == employeePassword && usernameText == employeeUsername) {
            warningMessage.SetActive(false);
            SceneManager.LoadScene(employeeScene);
        }

        else if (passwordText == managerPassword && usernameText == managerUsername) {
            warningMessage.SetActive(false);
            SceneManager.LoadScene(managerScene);
        }

        else { 
            if (warningMessage != null) {warningMessage.SetActive(true);} 
        }
    }

    public void ImportUsernameField(string text) {usernameText=text; Debug.Log(usernameText);}

    public void ImportPasswordField(string text) {passwordText=text; Debug.Log(passwordText);}


}
