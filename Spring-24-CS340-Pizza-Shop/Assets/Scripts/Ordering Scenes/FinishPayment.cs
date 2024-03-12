using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPayment : MonoBehaviour
{
    [SerializeField] private TMP_InputField _addressInputField;
    [SerializeField] private TMP_InputField _phoneNumberInputField;
    [SerializeField] private Transform _itemList;

    private double _orderTotal;
    private string _orderType;
    private string _orderDate;
    private string _orderTime;
    private string _address;
    private string _phoneNumber;

    private int _numberOfSmallCheesePizza;
    private int _numberOfSmallVegetablePizza;
    private int _numberOfSmallMeatPizza;

    private int _numberOfMediumCheesePizza;
    private int _numberOfMediumVegetablePizza;
    private int _numberOfMediumMeatPizza;

    private int _numberOfLargeCheesePizza;
    private int _numberOfLargeVegetablePizza;
    private int _numberOfLargeMeatPizza;

    /// <summary>
    /// This is called on Finish Payment button.
    /// </summary>
    public void FinishPaymentAndStoreInfo()
    {
        Debug.Log("<color=green>====================================================================================</color>");
        Debug.Log("Store these values to database");

        GetOrderType();
        GetOrderDateAndTime();
        GetAddressAndPhoneNumber();
        GetOrderItem();
        GetOrderTotal();

        Debug.Log("<color=green>====================================================================================</color>");

        // Back.RemoveCurrentScene(); // remove current scene so scene history is updated correct
        // TODO: on manager and employee screen, make a AccessLevel PlayerPrefs to know which scene to go to after payment is complete
        SceneManager.LoadScene(PlayerPrefs.GetString("AccessLevel"));
    }

    /// <summary>
    /// Get the order type by getting the scene name
    /// </summary>
    private void GetOrderType()
    {
        _orderType = SceneManager.GetActiveScene().name;
        Debug.Log("Order Type: " + _orderType);
    }

    /// <summary>
    /// Get order data and time by using System.DateTime.
    /// Split year, month, day, hour, minute, and second and concatenate data and time separately.
    /// </summary>
    private void GetOrderDateAndTime()
    {
        System.DateTime currentDateAndTime = System.DateTime.Now;

        int year = currentDateAndTime.Year;
        int month = currentDateAndTime.Month;
        int day = currentDateAndTime.Day;

        int hour = currentDateAndTime.Hour;
        int minute = currentDateAndTime.Minute;
        int second = currentDateAndTime.Second;

        _orderDate = month + "/" + day + "/" + year;
        _orderTime = hour + ":" + minute + ":" + second;

        Debug.Log("Order Date: " + _orderDate);
        Debug.Log("Order Time: " + _orderTime);
    }

    /// <summary>
    /// Gets the address and phone number by getting the text in the input field.
    /// Check if input fields are null so it works for all 3 ordering scenes.
    /// </summary>
    private void GetAddressAndPhoneNumber()
    {
        if (_addressInputField != null)
        {
            _address = _addressInputField.text;
            Debug.Log("Address: " + _address);
        }
        if (_phoneNumberInputField != null)
        {
            _phoneNumber = _phoneNumberInputField.text;
            Debug.Log("Phone Number: " + _phoneNumber);
        }
    }

    /// <summary>
    /// Loop through the item list and increment each pizza counter based on the number of the pizza in the order.
    /// </summary>
    private void GetOrderItem()
    {
        // look for number of each type of pizza in item list
        foreach (Transform pizzaTransform in _itemList)
        {
            GameObject pizza = pizzaTransform.gameObject;
            if (pizza.name.Contains("SmallCheese")) _numberOfSmallCheesePizza++;
            else if (pizza.name.Contains("SmallVegetable")) _numberOfSmallVegetablePizza++;
            else if (pizza.name.Contains("SmallMeat")) _numberOfSmallMeatPizza++;
            else if (pizza.name.Contains("MediumCheese")) _numberOfMediumCheesePizza++;
            else if (pizza.name.Contains("MediumVegetable")) _numberOfMediumVegetablePizza++;
            else if (pizza.name.Contains("MediumMeat")) _numberOfMediumMeatPizza++;
            else if (pizza.name.Contains("LargeCheese")) _numberOfLargeCheesePizza++;
            else if (pizza.name.Contains("LargeVegetable")) _numberOfLargeVegetablePizza++;
            else if (pizza.name.Contains("LargeMeat")) _numberOfLargeMeatPizza++;
        }

        Debug.Log("Small Cheese Pizza Count: " + _numberOfSmallCheesePizza);
        Debug.Log("Small Vegetable Pizza Count: " + _numberOfSmallVegetablePizza);
        Debug.Log("Small Meat Pizza Count: " + _numberOfSmallMeatPizza);

        Debug.Log("Medium Cheese Pizza Count: " + _numberOfMediumCheesePizza);
        Debug.Log("Medium Vegetable Pizza Count: " + _numberOfMediumVegetablePizza);
        Debug.Log("Medium Meat Pizza Count: " + _numberOfMediumMeatPizza);

        Debug.Log("Large Cheese Pizza Count: " + _numberOfLargeCheesePizza);
        Debug.Log("Large Vegetable Pizza Count: " + _numberOfLargeVegetablePizza);
        Debug.Log("Large Meat Pizza Count: " + _numberOfLargeMeatPizza);
    }

    /// <summary>
    /// Get the total of the order by calling the total variable in PriceManager.
    /// </summary>
    private void GetOrderTotal()
    {
        _orderTotal = OrderTotalManager.total;
        Debug.Log("Order Total: $" + _orderTotal);
    }
}