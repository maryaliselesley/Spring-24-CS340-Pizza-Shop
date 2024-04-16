using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPayment : MonoBehaviour
{
    [SerializeField] private TMP_InputField _addressInputField;
    [SerializeField] private TMP_InputField _phoneNumberInputField;
    [SerializeField] private Transform _itemList;

    [SerializeField] private GameObject _phoneWarningPopup; // Shows up when phone number is entered and does not confront the format
    [SerializeField] private TMP_Text _phoneWarningMessage;
    [SerializeField] private GameObject _addressWarningPopup; // Shows up when phone number is entered and does not confront the format
    [SerializeField] private TMP_Text _addressWarningMessage;

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
        // If phone or address is invalid, do not put anything in the database yet
        if (!IsPhoneNumberValid() || !IsAddressValid()) return;

        string items = GetOrderItem();
        string date = GetOrderDate();
        string time = GetOrderTime();
        string total = GetOrderTotal();
        string type = GetOrderType();
        string address = GetAddress();
        string phoneNumber = GetPhoneNumber();

        AddOrderToDatabase(items, date, time, total, type, address, phoneNumber);

        // Back.RemoveCurrentScene(); // remove current scene so scene history is updated correct
        // TODO: on manager and employee screen, make a AccessLevel PlayerPrefs to know which scene to go to after payment is complete
        // SceneManager.LoadScene(PlayerPrefs.GetString("AccessLevel"));
    }

    /// <summary>
    /// Add an order with specified order information to the database.
    /// </summary>
    /// <param name="orderItems"></param>
    /// <param name="orderDate"></param>
    /// <param name="orderTime"></param>
    /// <param name="orderTotal"></param>
    /// <param name="orderType"></param>
    /// <param name="orderAddress"></param>
    /// <param name="orderPhoneNumber"></param>
    private void AddOrderToDatabase(string orderItems, string orderDate, string orderTime, string orderTotal, string orderType, string orderAddress, string orderPhoneNumber)
    {
        string databaseName = "URI=file:OrderDatabase.db";

        using (var connection = new SqliteConnection(databaseName))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"INSERT INTO Orders (Content, Date, Time, Total, Type, Address, PhoneNumber) VALUES ('{orderItems}', '{orderDate}', '{orderTime}', '{orderTotal}', '{orderType}', '{orderAddress}', '{orderPhoneNumber}')";
                    command.ExecuteNonQuery(); // Used for SQL statements that don't return any data, such as INSERT, UPDATE, DELETE
                }
                transaction.Commit();
            }

            connection.Close();
        }
    }

    /// <summary>
    /// Loop through the item list and increment each pizza counter based on the number of the pizza in the order.
    /// </summary>
    private string GetOrderItem()
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

        string orderedPizza = ""; // A single string to store all ordered pizza, each in a separate line
        if (_numberOfSmallCheesePizza != 0) orderedPizza += "Small Cheese x" + _numberOfSmallCheesePizza + "\n";
        if (_numberOfSmallVegetablePizza != 0) orderedPizza += "Small Vege x" + _numberOfSmallVegetablePizza + "\n";
        if (_numberOfSmallMeatPizza != 0) orderedPizza += "Small Meat x" + _numberOfSmallMeatPizza + "\n";

        if (_numberOfMediumCheesePizza != 0) orderedPizza += "Medium Cheese x" + _numberOfMediumCheesePizza + "\n";
        if (_numberOfMediumVegetablePizza != 0) orderedPizza += "Medium Vege x" + _numberOfMediumVegetablePizza + "\n";
        if (_numberOfMediumMeatPizza != 0) orderedPizza += "Medium Meat x" + _numberOfMediumMeatPizza + "\n";

        if (_numberOfLargeCheesePizza != 0) orderedPizza += "Large Cheese x" + _numberOfLargeCheesePizza + "\n";
        if (_numberOfLargeVegetablePizza != 0) orderedPizza += "Large Vege x" + _numberOfLargeVegetablePizza + "\n";
        if (_numberOfLargeMeatPizza != 0) orderedPizza += "Large Meat x" + _numberOfLargeMeatPizza + "\n";

        return orderedPizza;
    }

    /// <summary>
    /// Get order month, day, and year by using System.DateTime.
    /// Split year, month, day, and concatenate them.
    /// <returns></returns>
    private string GetOrderDate()
    {
        System.DateTime currentDateAndTime = System.DateTime.Now;

        int year = currentDateAndTime.Year;
        int month = currentDateAndTime.Month;
        int day = currentDateAndTime.Day;

        return month + "/" + day + "/" + year;
    }

    /// <summary>
    /// Get order hour, minute, and second by using System.DateTime.
    /// Split hour, minute, second, and concatenate them.
    /// </summary>
    /// <returns></returns>
    private string GetOrderTime()
    {
        System.DateTime currentDateAndTime = System.DateTime.Now;
        int hour = currentDateAndTime.Hour;
        int minute = currentDateAndTime.Minute;
        int second = currentDateAndTime.Second;

        return hour + ":" + minute + ":" + second;
    }

    /// <summary>
    /// Get the total of the order by calling the total variable in PriceManager.
    /// <returns></returns>
    private string GetOrderTotal()
    {
        return "$" + OrderTotalManager.total.ToString();
    }

    /// <summary>
    /// Get the order type by getting the scene name
    /// <returns></returns>
    private string GetOrderType()
    {
        return SceneManager.GetActiveScene().name;
    }

    /// <summary>
    /// Gets the address by getting the text in the address input field. <br/>
    /// Check if input fields are null so it works for all 3 ordering scenes.
    /// <returns></returns>
    private string GetAddress()
    {
        if (_addressInputField != null) return _addressInputField.text;
        else return "";
    }

    /// <summary>
    /// Get the phone number by getting the text in the phone number input field. <br/>
    /// Check if input fields are null so it works for all 3 ordering scenes.
    /// </summary>
    /// <returns></returns>
    private string GetPhoneNumber()
    {
        if (_phoneNumberInputField != null) return _phoneNumberInputField.text;
        else return "";
    }

    /// <summary>
    /// Get phone number from the input field. If phone number is invalid, enable warning pop-up. <br/>
    /// Check if input fields are null so it works for all 3 ordering scenes.
    /// </summary>
    /// <returns></returns>
    private bool IsPhoneNumberValid()
    {
        if (_phoneNumberInputField != null)
        {
            if (!IsPhoneNumberValidHelper(_phoneNumberInputField.text))
            {
                _phoneWarningMessage.text = "Invalid phone number format. Double check to make sure it is not empty and is a 10-digit number without any letters or special characters.";
                _phoneWarningPopup.SetActive(true);
                return false;
            }
            else
            {
                return true;
            }
        }

        return true;
    }

    /// <summary>
    /// Check if a phone number is valid using regular expression to match exactly 10 digits with no other characters.
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    private bool IsPhoneNumberValidHelper(string phoneNumber)
    {
        Regex regex = new Regex(@"^\d{10}$");
        return regex.IsMatch(phoneNumber);
    }

    /// <summary>
    /// Check if a address is valid by look at if the input field is null. Pops a warning if address is not valid.
    /// </summary>
    /// <returns></returns>
    private bool IsAddressValid()
    {
        if (_phoneNumberInputField != null)
        {
            if (_addressInputField.text == "")
            {
                _addressWarningMessage.text = "Invalid address format. Double check to make sure the address is not empty.";
                _addressWarningPopup.SetActive(true);
                return false;
            }
            else return true;
        }

        return true;
    }
}