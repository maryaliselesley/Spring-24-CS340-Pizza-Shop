// Ignore Spelling: Username

using System.IO;
using TMPro;
using UnityEngine;
using System.Security.Cryptography;
using System;

/// <summary>
/// Currently does not support adding additional sets of login credentials and does not support changes to login credentials.
/// </summary>
public class UsernameAndPasswordManager : MonoBehaviour
{
    [Header("Only Used to Display Info")]
    [SerializeField] private LoginCredentials _loginCredentials;

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField _usernameField;
    [SerializeField] private TMP_InputField _passwordField;

    private string _managerUsername;
    private string _managerPassword;
    private string _employeeUsername;
    private string _employeePassword;

    private string _filePath; // Path that the json file will be stored at
    private string _encryptionKey = "AEi0z4rdZRrIY6N7zD/qvg=="; // Use generator for this
    private string _encryptionIV = "5zBz0dQ9p6eG6sRW"; // Use generator for this

    private void Start()
    {
        // Json file only gets created after Start() has been called (if doesn't already exist)
        _filePath = Path.Combine(Application.dataPath, "Login Credentials.json");
        LoadLoginCredentials();
    }

    /// <summary>
    /// Use Advanced Encryption Standard algorithm to encrypt login credentials.
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    private string EncryptString(string plainText, string key, string iv)
    {
        // Convert key and iv using UTF-8 encoding
        byte[] keyBytes = System.Text.Encoding.UTF8.GetBytes(key);
        byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(iv);

        byte[] encryptedBytes;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = keyBytes;
            aesAlg.IV = ivBytes;

            // Perform encryption with an encryptor
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }
                    encryptedBytes = memoryStream.ToArray();
                }
            }
        }
        return Convert.ToBase64String(encryptedBytes);
    }

    /// <summary>
    /// Use Advanced Encryption Standard algorithm to decrypt encrypted text to get login credentials.
    /// </summary>
    /// <param name="cipherText"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    private string DecryptString(string cipherText, string key, string iv)
    {
        // Convert encrypted text to bytes
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        byte[] keyBytes = System.Text.Encoding.UTF8.GetBytes(key);
        byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(iv);

        string plainText = null;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = keyBytes;
            aesAlg.IV = ivBytes;

            // Perform decryption with a decryptor
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream memoryStream = new MemoryStream(cipherBytes))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        plainText = streamReader.ReadToEnd();
                    }
                }
            }
        }

        return plainText; // Everything's stored in a single string
    }

    /// <summary>
    /// Take the Login Credential file and extract credentials out of it. If there is not a file, create a new file with default login credentials.
    /// </summary>
    private void LoadLoginCredentials()
    {
        if (File.Exists(_filePath))
        {
            string encryptedJson = File.ReadAllText(_filePath);
            string decryptedJson = DecryptString(encryptedJson, _encryptionKey, _encryptionIV);

            // Extract the usernames and passwords from the decrypted string
            _loginCredentials = JsonUtility.FromJson<LoginCredentials>(decryptedJson);
            _managerUsername = _loginCredentials.ManagerLoginCredentials.ManagerUsername;
            _managerPassword = _loginCredentials.ManagerLoginCredentials.ManagerPassword;
            _employeeUsername = _loginCredentials.EmployeeLoginCredentials.EmployeeUsername;
            _employeePassword = _loginCredentials.EmployeeLoginCredentials.EmployeePassword;

            Debug.Log($"Manager credentials are loaded. Username: {_managerUsername}, Password: {_managerPassword}");
            Debug.Log($"Employee credentials are loaded. Username: {_employeeUsername}, Password: {_employeePassword}");
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
    /// Save both manager and employee login credentials. <br/>
    /// Currently only used when there are no login credentials and a new set of login credentials are auto created.
    /// </summary>
    /// <param name="loginCredentials"></param>
    private void SaveLoginCredentials(LoginCredentials loginCredentials)
    {
        string json = JsonUtility.ToJson(loginCredentials);
        string encryptedJson = EncryptString(json, _encryptionKey, _encryptionIV);

        File.WriteAllText(_filePath, encryptedJson);
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

        if (_usernameField.text == _employeeUsername && _passwordField.text == _employeePassword)
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