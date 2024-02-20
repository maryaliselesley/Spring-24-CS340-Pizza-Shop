using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using TMPro;

public class Database : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _orderObjectPrefab;
    [SerializeField] private GameObject _orderPizza;

    private int counter = 0; // differentiate order objects

    private void Start()
    {
        DisplayDatabase();
    }

    private void DisplayDatabase()
    {
        string databaseName = "URI=file:OrderDatabase.db";

        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Orders";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // instantiate a new prefab and set it to child of scroll view with order information
                        GameObject orderObject = Instantiate(_orderObjectPrefab, _content.transform);
                        orderObject.name += counter.ToString();
                        counter++;
                        orderObject.transform.GetChild(0).GetComponent<TMP_Text>().text = reader["OrderID"].ToString();

                        // split the string of pizzas and instantiate each as a new object
                        string allOrderedPizzas = reader["Content"].ToString();
                        string[] lines = allOrderedPizzas.Split('\n');
                        foreach (string pizza in lines)
                        {
                            GameObject orderedPizza = Instantiate(_orderPizza, orderObject.transform.GetChild(1).transform);
                            orderedPizza.GetComponent<TMP_Text>().text = pizza;
                        }

                        orderObject.transform.GetChild(2).GetComponent<TMP_Text>().text = reader["Date"].ToString();
                        orderObject.transform.GetChild(3).GetComponent<TMP_Text>().text = reader["Time"].ToString();
                        orderObject.transform.GetChild(4).GetComponent<TMP_Text>().text = reader["Total"].ToString();
                        orderObject.transform.GetChild(5).GetComponent<TMP_Text>().text = reader["Type"].ToString();
                    }
                    reader.Close();
                }
            }

            connection.Close();
        }
    }

    public void RedisplayDatabase()
    {
        // destroy all order objects in content to clear history
        GameObject[] childObjects = GetChildGameObjects(_content);
        for (int i = 1; i < childObjects.Length; i++) // start at 1 because first position is the parent
        {
            Destroy(childObjects[i]);
        }

        // redisplay database content
        string databaseName = "URI=file:OrderDatabase.db";

        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Orders";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // instantiate a new prefab and set it to child of scroll view with order information
                        GameObject orderObject = Instantiate(_orderObjectPrefab, _content.transform);
                        orderObject.name += counter.ToString();
                        counter++;
                        orderObject.transform.GetChild(0).GetComponent<TMP_Text>().text = reader["OrderID"].ToString();

                        // split the string of pizzas and instantiate each as a new object
                        string allOrderedPizzas = reader["Content"].ToString();
                        string[] lines = allOrderedPizzas.Split('\n');
                        foreach (string pizza in lines)
                        {
                            GameObject orderedPizza = Instantiate(_orderPizza, orderObject.transform.GetChild(1).transform);
                            orderedPizza.GetComponent<TMP_Text>().text = pizza;
                        }

                        orderObject.transform.GetChild(2).GetComponent<TMP_Text>().text = reader["Date"].ToString();
                        orderObject.transform.GetChild(3).GetComponent<TMP_Text>().text = reader["Time"].ToString();
                        orderObject.transform.GetChild(4).GetComponent<TMP_Text>().text = reader["Total"].ToString();
                        orderObject.transform.GetChild(5).GetComponent<TMP_Text>().text = reader["Type"].ToString();
                    }
                    reader.Close();
                }
            }

            connection.Close();
        }
    }

    private GameObject[] GetChildGameObjects(GameObject parent)
    {
        // get all child gameobjects, including nested children
        Transform[] childTransforms = parent.GetComponentsInChildren<Transform>();

        // convert the transform array to a gameobject array
        GameObject[] childObjects = new GameObject[childTransforms.Length];
        for (int i = 0; i < childTransforms.Length; i++)
        {
            childObjects[i] = childTransforms[i].gameObject;
        }

        return childObjects;
    }

    public void Search(string orderNumber)
    {
        // destroy all order objects in content to clear history
        GameObject[] childObjects = GetChildGameObjects(_content);
        for (int i = 1; i < childObjects.Length; i++) // start at 1 because first position is the parent
        {
            Destroy(childObjects[i]);
        }

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
                    // instantiate a new prefab and set it to child of scroll view with order information
                    GameObject orderObject = Instantiate(_orderObjectPrefab, _content.transform);
                    orderObject.name += counter.ToString();
                    counter++;
                    orderObject.transform.GetChild(0).GetComponent<TMP_Text>().text = reader["OrderID"].ToString();

                    // split the string of pizzas and instantiate each as a new object
                    string allOrderedPizzas = reader["Content"].ToString();
                    string[] lines = allOrderedPizzas.Split('\n');
                    foreach (string pizza in lines)
                    {
                        GameObject orderedPizza = Instantiate(_orderPizza, orderObject.transform.GetChild(1).transform);
                        orderedPizza.GetComponent<TMP_Text>().text = pizza;
                    }

                    orderObject.transform.GetChild(2).GetComponent<TMP_Text>().text = reader["Date"].ToString();
                    orderObject.transform.GetChild(3).GetComponent<TMP_Text>().text = reader["Time"].ToString();
                    orderObject.transform.GetChild(4).GetComponent<TMP_Text>().text = reader["Total"].ToString();
                    orderObject.transform.GetChild(5).GetComponent<TMP_Text>().text = reader["Type"].ToString();

                    reader.Close();
                }
            }

            connection.Close();
        }
    }
}