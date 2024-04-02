using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using TMPro;
using UnityEngine.UI;

public class RevenueDatabase : MonoBehaviour
{
    public static RevenueDatabase instance;
    public GameObject orderHistoryContent; // Object that holds all order objects
    public GameObject orderObjectPrefab; // Object that has all order information
    public GameObject orderPizzaPrefab; // Pizza in the order

    [SerializeField]
    public GameObject textTotal;// Text for Total
    public GameObject textCount;// Text for displaying item count

    private int counter;

    private float total;



    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        counter=0;
        total=0.00f;

        textCount.GetComponent<TMP_Text>().text = counter.ToString();
        textTotal.GetComponent<TMP_Text>().text = "$" + total.ToString();

        DisplayDatabase();
    }

    /// <summary>
    /// Display orders by instantiating an orderObject prefab and change the text fields of all children to reflect the information of the order.
    /// If need to redisplay what orders needs to be on the canvas, set isRedisplay to true.
    /// Redisplay bool is used for searching order and adding order (adding order is test only currently)
    /// </summary>
    public void DisplayDatabase(string SQLCommand = "SELECT * FROM Orders")
    {
        DestroyOnScreenOrderObjects();

        string databaseName = "URI=file:OrderDatabase.db";

        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = SQLCommand;

                using (IDataReader reader = command.ExecuteReader())
                {
                    InstantiateOrderObjects(reader);
                    reader.Close();
                }
            }
            connection.Close();
        }
    }

    public void SelectAllOrders()
    {
        DisplayDatabase("SELECT * FROM Orders");
    }

    public void SelectDineInOrders()
    {
        DisplayDatabase("SELECT * FROM Orders WHERE Type=\"DineIn\"");
    }

    public void SelectTakeOutOrder()
    {
        DisplayDatabase("SELECT * FROM Orders WHERE Type=\"TakeOut\"");
    }

    public void SelectDeliveryOrder()
    {
        DisplayDatabase("SELECT * FROM Orders WHERE Type=\"Delivery\"");
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
            counter++;
            // Instantiate a new prefab and set it to child of "content" GameObject
            GameObject orderObject = Instantiate(orderObjectPrefab, orderHistoryContent.transform);
            orderObject.transform.GetChild(0).GetComponent<TMP_Text>().text = reader["OrderID"].ToString();

            // Split the string of pizzas based on lines
            string stringOfPizza = reader["Content"].ToString();
            string[] allOrderedPizza = stringOfPizza.Split('\n');

            foreach (string orderedPizza in allOrderedPizza)
            {
                // Instantiate the GameObject to as a child of orderObject and set its text to the pizza info from database
                GameObject pizza = Instantiate(orderPizzaPrefab, orderObject.transform.GetChild(1).transform);
                pizza.GetComponent<TMP_Text>().text = orderedPizza;

            }

            // Set each field of the order object to reflect what's in the database
            orderObject.transform.GetChild(2).GetComponent<TMP_Text>().text = reader["Date"].ToString();
            orderObject.transform.GetChild(3).GetComponent<TMP_Text>().text = reader["Time"].ToString();
            orderObject.transform.GetChild(4).GetComponent<TMP_Text>().text = reader["Total"].ToString();
            orderObject.transform.GetChild(5).GetComponent<TMP_Text>().text = reader["Type"].ToString();
            orderObject.transform.GetChild(6).GetComponent<TMP_Text>().text = reader["Address"].ToString();
            orderObject.transform.GetChild(7).GetComponent<TMP_Text>().text = reader["PhoneNumber"].ToString();

            Debug.Log(reader["Total"].ToString());
           //Add to total, remove the dollar sign before operation
            total += float.Parse(reader["Total"].ToString().Remove(0,1));

            textCount.GetComponent<TMP_Text>().text = counter.ToString();
            textTotal.GetComponent<TMP_Text>().text = "$" + total.ToString();

        }
    }

    /// <summary>
    /// Destroy all order objects in "content" GameObject to remove order objects on canvas currently
    /// </summary>
    public void DestroyOnScreenOrderObjects()
    {
        GameObject[] orderObjects = GetChildGameObjects(orderHistoryContent);

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