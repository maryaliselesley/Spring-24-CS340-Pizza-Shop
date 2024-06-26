// Ignore Spelling: Prefs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAllPlayerPrefKeys : MonoBehaviour
{
    private string[] _keys = new string[]
    {
        "smallPizzaPrice",
        "mediumPizzaPrice",
        "largePizzaPrice"
    };

    /// <summary>
    /// Set the values of player prefs keys
    /// </summary>
    public void SetPlayerPrefsValues()
    {
        PlayerPrefs.SetFloat("smallPizzaPrice", 4.99f);
        PlayerPrefs.SetFloat("mediumPizzaPrice", 9.99f);
        PlayerPrefs.SetFloat("largePizzaPrice", 14.99f);
    }

    /// <summary>
    /// Delete all stored keys in player prefs.
    /// </summary>
    public void DeleteAllKeys()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log($"<color=green>=============================================</color>");
        foreach (string key in _keys)
        {
            DeleteKey(key);
        }
        Debug.Log($"<color=green>=============================================</color>");
    }

    /// <summary>
    /// Delete a particular key, specified by the keyName parameter.
    /// </summary>
    /// <param name="keyName"></param>
    private void DeleteKey(string keyName)
    {
        if (PlayerPrefs.HasKey(keyName))
        {
            PlayerPrefs.DeleteKey(keyName);
            Debug.Log($"<color=green>The key \"" + keyName + "\" has been deleted</color>");
        }
        else
        {
            Debug.Log($"<color=red>The key \"" + keyName + "\" does not exist</color>");
        }
    }
}