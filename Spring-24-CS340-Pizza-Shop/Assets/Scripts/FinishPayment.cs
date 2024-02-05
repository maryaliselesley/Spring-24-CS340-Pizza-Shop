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

    private int numberOfSmallCheesePizza;
    private int numberOfSmallVegetablePizza;
    private int numberOfSmallMeatPizza;

    private int numberOfMediumCheesePizza;
    private int numberOfMediumVegetablePizza;
    private int numberOfMediumMeatPizza;

    private int numberOfLargeCheesePizza;
    private int numberOfLargeVegetablePizza;
    private int numberOfLargeMeatPizza;

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

        Back.RemoveCurrentScene(); // remove current scene so scene history is updated correctly
        // TODO: on manager and employee screen, make a AccessLevel PlayerPrefs to know which scene to go to after payment is complete
        SceneManager.LoadScene(PlayerPrefs.GetString("AccessLevel"));
    }

    private void GetOrderType()
    {
        _orderType = SceneManager.GetActiveScene().name;
        Debug.Log("Order Type: " + _orderType);
    }

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

    private void GetAddressAndPhoneNumber()
    {
        // check if input fields are null so this script works for all 3 ordering scenes
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

    private void GetOrderItem()
    {
        // look for number of each type of pizza in item list
        foreach (Transform pizzaTransform in _itemList)
        {
            GameObject pizza = pizzaTransform.gameObject;
            if (pizza.name.Contains("SmallCheese")) numberOfSmallCheesePizza++;
            else if (pizza.name.Contains("SmallVegetable")) numberOfSmallVegetablePizza++;
            else if (pizza.name.Contains("SmallMeat")) numberOfSmallMeatPizza++;
            else if (pizza.name.Contains("MediumCheese")) numberOfMediumCheesePizza++;
            else if (pizza.name.Contains("MediumVegetable")) numberOfMediumVegetablePizza++;
            else if (pizza.name.Contains("MediumMeat")) numberOfMediumMeatPizza++;
            else if (pizza.name.Contains("LargeCheese")) numberOfLargeCheesePizza++;
            else if (pizza.name.Contains("LargeVegetable")) numberOfLargeVegetablePizza++;
            else if (pizza.name.Contains("LargeMeat")) numberOfLargeMeatPizza++;
        }

        Debug.Log("Small Cheese Pizza Count: " + numberOfSmallCheesePizza);
        Debug.Log("Small Vegetable Pizza Count: " + numberOfSmallVegetablePizza);
        Debug.Log("Small Meat Pizza Count: " + numberOfSmallMeatPizza);

        Debug.Log("Medium Cheese Pizza Count: " + numberOfMediumCheesePizza);
        Debug.Log("Medium Vegetable Pizza Count: " + numberOfMediumVegetablePizza);
        Debug.Log("Medium Meat Pizza Count: " + numberOfMediumMeatPizza);

        Debug.Log("Large Cheese Pizza Count: " + numberOfLargeCheesePizza);
        Debug.Log("Large Vegetable Pizza Count: " + numberOfLargeVegetablePizza);
        Debug.Log("Large Meat Pizza Count: " + numberOfLargeMeatPizza);
    }

    private void GetOrderTotal()
    {
        _orderTotal = PriceManager.total;
        Debug.Log("Order Total: $" + _orderTotal);
    }
}