// Ignore Spelling: Username

using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;

public class UsernameAndPasswordManager : MonoBehaviour
{
    [Header("Only Used to Display Info")]
    [SerializeField] private LoginCredentials _loginCredentials;

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField _usernameField;
    [SerializeField] private TMP_InputField _passwordField;

    private string _managerUsername;
    private string _managerPassword;
    private string _employeerUsername;
    private string _employeePassword;

    private string _filePath;
    private string _encryptionKey = "EKey";
    private string _encryptionIV = "EIV";

    private void Start()
    {
        _filePath = Path.Combine(Application.dataPath, "Login Credentials.json");
        LoadLoginCredentials();
    }

    /// <summary>
    /// Take the Login Credential file and extract credentials out of it. If there is not a file, create a new file with default login credentials.
    /// </summary>
    private void LoadLoginCredentials()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);

            // Deserialize JSON and pull data out of it
            _loginCredentials = JsonUtility.FromJson<LoginCredentials>(json);
            _managerUsername = _loginCredentials.ManagerLoginCredentials.ManagerUsername;
            _managerPassword = _loginCredentials.ManagerLoginCredentials.ManagerPassword;
            _employeerUsername = _loginCredentials.EmployeeLoginCredentials.EmployeeUsername;
            _employeePassword = _loginCredentials.EmployeeLoginCredentials.EmployeePassword;

            Debug.Log($"Manager credentials are loaded. Username: {_managerUsername}, Password: {_managerPassword}");
            Debug.Log($"Employee credentials are loaded. Username: {_employeerUsername}, Password: {_employeePassword}");
        }
        else
        {
            Debug.Log("File does not exist, creating new credentials...");

            // Create default credentials
            _loginCredentials = new LoginCredentials();
            _loginCredentials.ManagerLoginCredentials.ManagerUsername = "Manager";
            _loginCredentials.ManagerLoginCredentials.ManagerPassword = "m1234";


            _loginCredentials.EmployeeLoginCredentials.EmployeeUsername = "Employee";
            _loginCredentials.EmployeeLoginCredentials.EmployeePassword = "e1234";

            SaveLoginCredentials(_loginCredentials);
        }
    }

    /// <summary>
    /// Save both manager and employee login credentials.
    /// </summary>
    /// <param name="loginCredentials"></param>
    private void SaveLoginCredentials(LoginCredentials loginCredentials)
    {
        string json = JsonUtility.ToJson(loginCredentials);

        File.WriteAllText(_filePath, json);
    }

    // TODO: Check for login. 2 if statements are not enough.
    public void Login()
    {
        if (_usernameField.text == _managerUsername && _passwordField.text == _managerPassword)
        {
            Debug.Log("Manager Login Successful From UsernameAndPassowrdManager");
        }
        else
        {
            Debug.Log("Manager Login Failed From UsernameAndPassowrdManager");
        }

        if (_usernameField.text == _employeerUsername && _passwordField.text == _employeePassword)
        {
            Debug.Log("Employee Login Successful From UsernameAndPassowrdManager");
        }
        else
        {
            Debug.Log("Employee Login Failed From UsernameAndPassowrdManager");
        }
    }

}

[System.Serializable]
public class LoginCredentials
{
    public ManagerUsernameAndPassword ManagerLoginCredentials;
    public EmployeeUsernameAndPassword EmployeeLoginCredentials;

    public LoginCredentials()
    {
        ManagerLoginCredentials = new ManagerUsernameAndPassword();
        EmployeeLoginCredentials = new EmployeeUsernameAndPassword();
    }
}

[System.Serializable]
public class ManagerUsernameAndPassword
{
    public string ManagerUsername;
    public string ManagerPassword;
}

[System.Serializable]
public class EmployeeUsernameAndPassword
{
    public string EmployeeUsername;
    public string EmployeePassword;
}