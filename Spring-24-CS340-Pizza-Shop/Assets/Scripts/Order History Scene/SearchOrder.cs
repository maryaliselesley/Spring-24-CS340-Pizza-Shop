using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using System;
using System.Text.RegularExpressions;

public class SearchOrder : MonoBehaviour
{
    private TMP_InputField _searchInputField;

    [SerializeField] private GameObject _content; // GameObject that holds all order objects on canvas
    [SerializeField] private TMP_Text _warningMessage;

    private void Awake()
    {
        _searchInputField = GetComponent<TMP_InputField>();
    }

    private void Start()
    {
        _warningMessage.gameObject.SetActive(false);
    }

    /// <summary>
    /// This is called on "On Value Changed" in the Input Field component. <br/>
    /// Input a number into the search box and display the order based on the order number. <br/>
    /// Any non-digit character will not trigger searching.
    /// </summary>
    public void SearchForOrder()
    {
        _warningMessage.gameObject.SetActive(false);

        Regex regex = new Regex(@"^\d+$");

        // If nothing is in the search box, redisplay database
        if (_searchInputField.text == "")
        {
            OrderHistoryDatabase.instance.DisplayDatabase();
            return;
        }

        // If input is not valid, display the whole database and display a warning
        if (!regex.IsMatch(_searchInputField.text))
        {
            _warningMessage.text = "Input is incorrect. Make sure it's numbers only.";
            _warningMessage.gameObject.SetActive(true);
            OrderHistoryDatabase.instance.DisplayDatabase();
            return;
        }

        // If something is in the search box and is a valid input, then display the order according to the digit
        if (regex.IsMatch(_searchInputField.text))
        {
            string databaseName = "URI=file:OrderDatabase.db";
            using (var connection = new SqliteConnection(databaseName))
            {
                connection.Open();
                StartCoroutine(Search(_searchInputField.text));
            }
        }
    }

    /// <summary>
    /// Search for an order based on given order number
    /// </summary>
    /// <param name="orderNumber"></param>
    private IEnumerator Search(string orderNumber)
    {
        // Destroy the on screen order objects
        OrderHistoryDatabase.instance.DestroyOnScreenOrderObjects();

        // Redisplay database content
        string databaseName = "URI=file:OrderDatabase.db";

        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM Orders Where OrderID={orderNumber}";

                using (IDataReader reader = command.ExecuteReader())
                {
                    OrderHistoryDatabase.instance.InstantiateOrderObjects(reader); // Show the order by instantiating an order prefab
                    reader.Close();
                }
            }

            connection.Close();
        }

        yield return new WaitForEndOfFrame(); // Wait until objects are properly deleted

        // If there's only the header under content, then there's not order associated with that order number
        if (_content.transform.childCount <= 1)
        {
            _warningMessage.text = $"Order {orderNumber} does not exist.";
            _warningMessage.gameObject.SetActive(true);
        }
    }
}