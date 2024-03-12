using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Employeeinformationmanager : MonoBehaviour
{
    [SerializeField] private TMP_InputField Name;
    [SerializeField] private TMP_InputField SSN;
    [SerializeField] private TMP_InputField Title;
    [SerializeField] private TMP_InputField Phone;
    private string input;

    private void Update()
    {
        Debug.Log(Name.text);
        Debug.Log(SSN.text);
        Debug.Log(Title.text);
        Debug.Log(Phone.text);
    }

    public void SaVeEmpInfo()
    {
        PlayerPrefs.SetString("Employee1", Name.text);
        PlayerPrefs.SetString("Employee1", SSN.text);
        PlayerPrefs.SetString("Employee1", Title.text);
        PlayerPrefs.SetString("Employee1", Phone.text);
    }
    }
