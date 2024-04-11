using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

// This is for testing only!!!
public class AddTestOrderToDatabase : MonoBehaviour
{
    [SerializeField] private Scrollbar _verticalScrollbar;

    public void AddTestOrder()
    {
        string databaseName = "URI=file:OrderDatabase.db";

        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Orders (Content, Date, Time, Total, Type, Address, PhoneNumber) VALUES ('Test Pizza x1', '12/10/2025', '20:20PM', '$99.00', 'Test Type', '1234 Test Testing Drive', '123-456-7890')";
                    command.ExecuteNonQuery(); // Used for SQL statements that don't return any data, such as INSERT, UPDATE, DELETE
                }
                transaction.Commit();
            }

            connection.Close();
        }

        OrderHistoryDatabase.instance.DisplayDatabase(true);
    }

    public void UpdateScrollBar()
    {
        _verticalScrollbar.value = 0;
    }
}