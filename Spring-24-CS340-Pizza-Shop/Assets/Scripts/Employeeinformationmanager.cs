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

    public int EmployeeNumber;

    public void DisplayEmpInfo()
    {
        Name.text = PlayerPrefs.GetString("Employee" + EmployeeNumber.ToString() + "Name");
        SSN.text = PlayerPrefs.GetString("Employee" + EmployeeNumber.ToString() + "SSN");
        Title.text = PlayerPrefs.GetString("Employee" + EmployeeNumber.ToString() + "Title");
        Phone.text = PlayerPrefs.GetString("Employee" + EmployeeNumber.ToString() + "Phone");
    }

    public void SetEmployeeNumber(int number)
    {
        EmployeeNumber = number;
    }

    public void SaVeEmpInfo()
    {
        PlayerPrefs.SetString("Employee" + EmployeeNumber.ToString() + "Name" , Name.text);
        PlayerPrefs.SetString("Employee" + EmployeeNumber.ToString() + "SSN" , SSN.text);
        PlayerPrefs.SetString("Employee" + EmployeeNumber.ToString() + "Title", Title.text);
        PlayerPrefs.SetString("Employee" + EmployeeNumber.ToString() + "Phone", Phone.text);

    }

    public void DeleteEmpInfo()
    {
        PlayerPrefs.DeleteKey("Employee" + EmployeeNumber.ToString() + "Name");
        PlayerPrefs.DeleteKey("Employee" + EmployeeNumber.ToString() + "SSN");
        PlayerPrefs.DeleteKey("Employee" + EmployeeNumber.ToString() + "Title");
        PlayerPrefs.DeleteKey("Employee" + EmployeeNumber.ToString() + "Phone");

        Name.text = " ";
        SSN.text = " ";
        Title.text = " ";
        Phone.text = " ";
    }
    }
