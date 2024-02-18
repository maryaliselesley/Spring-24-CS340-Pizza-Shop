using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExitProgram : MonoBehaviour
{
    public void CloseProgram() {

        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        Debug.Log("Program would exit now if it was compiled.");
        
        #else
        Application.Quit();
        #endif
    }
}
