using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using TMPro;
using UnityEngine.UI;

public class OrderHistoryDatabase : MonoBehaviour
{
    public static OrderHistoryDatabase Instance;
    [SerializeField] private GameObject _orderHistoryContent; // Object that holds all order objects
    [SerializeField] private GameObject _orderObjectPrefab; // Object that has all order information
    [SerializeField] private GameObject _orderPizzaPrefab; // Pizza in the order

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        DisplayDatabase();
    }

    /// <summary>
    /// Display orders by destroy any order objects and instantiating order objects and change the text fields of all children to reflect the information of the order.
    /// </summary>
    public void DisplayDatabase()
    {
        DestroyOnScreenOrderObjects();

        string databaseName = "URI=file:OrderDatabase.db";

        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Orders";

                using (IDataReader reader = command.ExecuteReader())
                {
                    InstantiateOrderObjects(reader);
                    reader.Close();
                }
            }

            connection.Close();
        }
    }

    /// <summary>
    /// Instantiate order objects with the information from database.
    /// Reader will go through the table and reads each cell of the table.
    /// Children of order objects must follow a specific sequence: OrderID, Pizza, Data, Time, Total, Type, Address, Phone Number
    /// </summary>
    /// <param name="reader"></param>
    public void InstantiateOrderObjects(IDataReader reader)
    {
        while (reader.Read())
        {
            // Instantiate a new prefab and set it to child of "content" GameObject
            GameObject orderObject = Instantiate(_orderObjectPrefab, _orderHistoryContent.transform);
            orderObject.transform.GetChild(0).GetComponent<TMP_Text>().text = reader["OrderID"].ToString();

            // Split the string of pizzas based on lines
            string stringOfPizza = reader["Content"].ToString();
            string[] allOrderedPizza = stringOfPizza.Split('\n');

            foreach (string orderedPizza in allOrderedPizza)
            {
                // Instantiate the GameObject to as a child of orderObject and set its text to the pizza info from database
                GameObject pizza = Instantiate(_orderPizzaPrefab, orderObject.transform.GetChild(1).transform);
                pizza.GetComponent<TMP_Text>().text = orderedPizza;
            }

            // Set each field of the order object to reflect what's in the database
            orderObject.transform.GetChild(2).GetComponent<TMP_Text>().text = reader["Date"].ToString();
            orderObject.transform.GetChild(3).GetComponent<TMP_Text>().text = reader["Time"].ToString();
            orderObject.transform.GetChild(4).GetComponent<TMP_Text>().text = reader["Total"].ToString();
            orderObject.transform.GetChild(5).GetComponent<TMP_Text>().text = reader["Type"].ToString();
            orderObject.transform.GetChild(6).GetComponent<TMP_Text>().text = reader["Address"].ToString();
            orderObject.transform.GetChild(7).GetComponent<TMP_Text>().text = reader["PhoneNumber"].ToString();
        }
    }

    /// <summary>
    /// Destroy all order objects in "content" GameObject to remove order objects on canvas currently
    /// </summary>
    public void DestroyOnScreenOrderObjects()
    {
        GameObject[] orderObjects = GetChildGameObjects(_orderHistoryContent);

        for (int i = 1; i < orderObjects.Length; i++)
        {
            Destroy(orderObjects[i]);
        }
    }

    /// <summary>
    /// Gets all child GameObjects of a parent by getting the total child count of a parent.
    /// Then loop through all children and put them into the array
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    private GameObject[] GetChildGameObjects(GameObject parent)
    {
        int childCount = parent.transform.childCount;
        GameObject[] childObjects = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            GameObject childGameObject = parent.transform.GetChild(i).gameObject; // Convert the child to GameObject by getting their transform and call .gameObject on it
            childObjects[i] = childGameObject;
        }

        return childObjects;
    }
}