using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class SearchOrder : MonoBehaviour
{
    private TMP_InputField _searchInputField;
    private bool _isDisplayingAllOrders = true; // Used to control order displayed after using search

    private void Awake()
    {
        _searchInputField = GetComponent<TMP_InputField>();
    }

    /// <summary>
    /// This is called on "On Value Changed" in the Input Field component
    /// Input a number into the search box and display the order based on the order number.
    /// Any non-digit character will not trigger searching.
    /// </summary>
    public void SearchForOrder()
    {
        // If nothing is in the search box and not currently displaying orders, redisplay database and make the bool true
        if (_searchInputField.text == "" && !_isDisplayingAllOrders)
        {
            OrderHistoryDatabase.instance.DisplayDatabase(true);
            _isDisplayingAllOrders = true;
            return;
        }

        // If something is in the search box and is a digit, then show display the order according to the digit
        if (int.TryParse(_searchInputField.text, out int result))
        {
            _isDisplayingAllOrders = false;
            string databaseName = "URI=file:OrderDatabase.db";
            using (var connection = new SqliteConnection(databaseName))
            {
                connection.Open();

                Search(_searchInputField.text);
            }
        }
    }

    /// <summary>
    /// Search for an order based on given order number
    /// </summary>
    /// <param name="orderNumber"></param>
    private void Search(string orderNumber)
    {
        OrderHistoryDatabase.instance.DestroyOnScreenOrderObjects();

        // redisplay database content
        string databaseName = "URI=file:OrderDatabase.db";

        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM Orders Where OrderID={orderNumber}";

                using (IDataReader reader = command.ExecuteReader())
                {
                    OrderHistoryDatabase.instance.InstantiateOrderObjects(reader);

                    reader.Close();
                }
            }

            connection.Close();
        }
    }
}