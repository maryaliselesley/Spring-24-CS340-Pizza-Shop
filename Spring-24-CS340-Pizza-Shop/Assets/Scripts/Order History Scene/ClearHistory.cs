using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class ClearHistory : MonoBehaviour
{
    /// <summary>
    /// Assigned to "Clear History" button
    /// Once clicked, all orders in the data base will be deleted and GameObjects that displays order on the canvas will be destroyed
    /// </summary>
    public void ClearHistoryButton()
    {
        string databaseName = "URI=file:OrderDatabase.db";

        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            // Deletes all orders
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Orders";
                command.ExecuteNonQuery(); // Used for SQL statements that don't return any data, such as INSERT, UPDATE, DELETE
            }

            // Resets auto increment
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM sqlite_sequence WHERE name='Orders'";
                command.ExecuteNonQuery(); // Used for SQL statements that don't return any data, such as INSERT, UPDATE, DELETE
            }

            OrderHistoryDatabase.instance.DestroyOnScreenOrderObjects();

            connection.Close();
        }
    }
}