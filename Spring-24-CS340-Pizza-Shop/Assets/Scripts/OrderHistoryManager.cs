using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using TMPro;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using Transform = UnityEngine.Transform;

public class OrderHistoryManager : MonoBehaviour
{
    private GameObject _orderObject; // object that holds all order info
    [SerializeField] private GameObject _content; // object that holds all order objects
    [SerializeField] private Database _database; // reference to update database
    [SerializeField] private TMP_InputField _inputField; // used for searching
    private bool _isDisplayingAllOrders = true; // used to control order displayed after using search

    // update will manage order selection
    private void Update()
    {
        SearchForOrder();
        // selection only works when clicking on a part of a text component
        if (Input.GetMouseButtonDown(0))
        {
            // cast a ray from the mouse position using EventSystem
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            // raycast to detect UI hits
            EventSystem.current.RaycastAll(eventData, results);

            if (results.Count > 0)
            {
                GameObject selectedUIObject = results[0].gameObject;
                Transform parentTransform = selectedUIObject.transform.parent;

                // if the game object does not have DynamicRectTransformHeight script, it's "Pizza" so go one parent higher
                if (parentTransform.gameObject.GetComponent<DynamicRectTransformHeight>() == null)
                {
                    parentTransform = parentTransform.transform.parent;
                }

                // prevent selecting other ui game objects
                if (selectedUIObject.name == "Viewport")
                {
                    // deselect the currently selected object
                    if (_orderObject != null)
                    {
                        _orderObject.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0f);
                        _orderObject = null;
                        Debug.Log("Deselected Object");
                    }
                }
                // if select object's parent is order object, enable alpha
                else if (parentTransform != null && parentTransform.name.Contains("Order Object"))
                {
                    _orderObject = parentTransform.gameObject;
                    _orderObject.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0.4f);
                    Debug.Log("Selected Object: " + _orderObject.name);
                }
                else
                {
                    Debug.Log("Selected object is weird: " + selectedUIObject);
                    return;
                }
            }
        }

        // unselect all other order objects
        if (_content.transform.childCount > 0)
        {
            for (int i = 0; i < _content.transform.childCount; i++)
            {
                Transform child = _content.transform.GetChild(i);

                if (_orderObject != null && child != null)
                {
                    if (child.name != _orderObject.name && child.GetComponent<RawImage>() != null)
                    {
                        child.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, 0f);
                    }
                }
            }
        }
    }

    // this right now is for testing only
    public void EditOrder()
    {
        string databaseName = "URI=file:OrderDatabase.db";

        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Orders (Content, Date, Time, Total, Type) VALUES ('Test Pizza x1', '12/10/2025', '12:10PM', '$10.10', 'Delivery')";
                    command.ExecuteNonQuery(); // used for SQL statements that don't return any data, such as INSERT, UPDATE, DELETE
                }
                transaction.Commit();
            }

            connection.Close();
        }

        UpdateOrderHistory();
    }

    public void RemoveOrder()
    {
        if (_orderObject != null)
        {
            string orderNumber = _orderObject.transform.GetChild(0).GetComponent<TMP_Text>().text; // order number is always the first child
            string databaseName = "URI=file:OrderDatabase.db";

            using (var connection = new SqliteConnection(databaseName))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"DELETE FROM Orders WHERE OrderID={orderNumber}"; // deletes order based on order number
                        command.ExecuteNonQuery(); // used for SQL statements that don't return any data, such as INSERT, UPDATE, DELETE
                    }

                    transaction.Commit(); // commit the changes so the database updates properly
                }

                connection.Close();
            }
        }

        Destroy(_orderObject);
    }

    public void ClearHistory()
    {
        string databaseName = "URI=file:OrderDatabase.db";

        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Orders"; // deletes all orders
                command.ExecuteNonQuery(); // used for SQL statements that don't return any data, such as INSERT, UPDATE, DELETE
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM sqlite_sequence WHERE name='Orders'"; // resets auto increment
                command.ExecuteNonQuery(); // used for SQL statements that don't return any data, such as INSERT, UPDATE, DELETE
            }

            // destroy all order objects in content to clear history
            GameObject[] childObjects = GetChildGameObjects(_content);
            for (int i = 1; i < childObjects.Length; i++) // start at 1 because first position is the parent
            {
                Destroy(childObjects[i]);
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

    private void UpdateOrderHistory()
    {
        _database.RedisplayDatabase();
    }

    private void SearchForOrder()
    {
        if (_inputField.text == "" && !_isDisplayingAllOrders)
        {
            _database.RedisplayDatabase();
            _isDisplayingAllOrders = true;
            return;
        }

        if (int.TryParse(_inputField.text, out int result))
        {
            _isDisplayingAllOrders = false;
            string databaseName = "URI=file:OrderDatabase.db";
            using (var connection = new SqliteConnection(databaseName))
            {
                connection.Open();

                _database.Search(_inputField.text);
            }
        }
    }
}