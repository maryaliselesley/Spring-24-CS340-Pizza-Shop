using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class InputFieldTabbing : MonoBehaviour
{
    public TMP_InputField username; //item 0
    public TMP_InputField password; //item 1

    public Button signIn;

    enum state 
        {
            USERNAME,
            PASSWORD
        }

    state currentState = state.USERNAME;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (currentState == state.USERNAME) {
                currentState=state.PASSWORD;
                password.Select();
                }
            else if (currentState == state.PASSWORD){
                currentState=state.USERNAME;  
                username.Select();}
        }

        if (Input.GetKeyDown(KeyCode.Return)) {signIn.onClick.Invoke();}

    }

    private void SelectUsername() {}

    private void SelectPassword() {}
}
