using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RemoveOrder : MonoBehaviour
{
    private GameObject _selectedOrder;

    private void Update()
    {
        SelectObject();
    }

    /// <summary>
    /// Select object by casting a ray on canvas and loop through the results to find the object that has the script "DynamicRectTransformHeight".
    /// Only order objects should have the script "DynamicRectTransformHeight".
    /// </summary>
    private void SelectObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> raycastResults = new List<RaycastResult>();

            // Cast a ray from the mouse position using EventSystem to detect UI hits
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            // Loop through raycast results to find order object
            foreach (RaycastResult raycastResult in raycastResults)
            {
                GameObject order = raycastResult.gameObject;

                if (order.name.Contains("RemoveOrder")) break; // If RemoveOrder button is hit, break so _selectedOrder is not nullified before RemoveOrder is clicked

                // Only the order object has the DynamicRectTransformHeight script, so if it's not null, it will be the object that we're looking for
                if (order.GetComponent<DynamicRectTransformHeight>() != null)
                {
                    _selectedOrder = order;
                    break;
                }
                else
                {
                    _selectedOrder = null;
                }
            }
        }
    }

    /// <summary>
    /// Assigned to "Remove Order" button.
    /// Once clicked, order in the database will be deleted based on order number and selected GameObject will be destroyed on canvas.
    /// </summary>
    public void RemoveOrderButton()
    {
        // Need to ensure an order is selected before executing remove command
        if (_selectedOrder != null)
        {
            string orderNumber = _selectedOrder.transform.GetChild(0).GetComponent<TMP_Text>().text; // Order number is always the first child
            string databaseName = "URI=file:OrderDatabase.db";

            using (var connection = new SqliteConnection(databaseName))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"DELETE FROM Orders WHERE OrderID={orderNumber}"; // Deletes order based on order number
                        command.ExecuteNonQuery(); // Used for SQL statements that don't return any data, such as INSERT, UPDATE, DELETE
                    }

                    transaction.Commit(); // Commit the changes so the database updates properly
                }

                connection.Close();
            }

            Destroy(_selectedOrder);
            _selectedOrder = null;
        }
    }
}