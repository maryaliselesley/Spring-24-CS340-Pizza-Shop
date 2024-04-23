using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrderTotalManager : MonoBehaviour
{
    public static OrderTotalManager instance;

    // Fees
    private const double _deliveryFee = 3;
    private const double _dineInServiceFee = 0.05;

    private static double _fee = 0;
    private static double _taxPercentage = 0.1f;
    private static double _subTotal = 0;
    public static double total = 0;

    [SerializeField] private TMP_Text _subTotalText;
    [SerializeField] private TMP_Text _totalText;
    [SerializeField] private TMP_Text _feeText; // This will stay null and unused in "Take Out"

    private void Awake()
    {
        instance = this;
    }

    // Ideally, prices gets updated only when a pizza is added or removed
    // But due to the order of execution, pizza counts do not always update correctly when pizzas are being removed
    public void Update()
    {
        CalculateFee();

        CalculateSubTotal();
        _subTotalText.text = "Subtotal: $" + _subTotal.ToString(); // Updates sub total text on canvas

        CalculateTotal();
        _totalText.text = "Total: $" + total.ToString(); // Updates total text on canvas
    }

    /// <summary>
    /// Calculate and update fee based on the scene name, which the scene name determines the order type
    /// </summary>
    private void CalculateFee()
    {
        if (SceneManager.GetActiveScene().name == "Delivery")
        {
            _fee = _deliveryFee;
            _feeText.text = "Delivery Fee: $" + _fee.ToString();
        }
        else if (SceneManager.GetActiveScene().name == "Dine In")
        {
            _fee = _dineInServiceFee * total;
            _fee = RoundToTwoDecimalPlaces(_fee);
            _feeText.text = "Service Fee: $" + _fee.ToString();
        }
    }

    /// <summary>
    /// Calculate subtotal by getting the total numbers of each type of pizza, calculate the total of each, and then add them up
    /// </summary>
    private void CalculateSubTotal()
    {
        // Get total number of each type of pizza using tags
        GameObject[] numberOfSmallPizza = GameObject.FindGameObjectsWithTag("Small Pizza");
        GameObject[] numberOfMediumPizza = GameObject.FindGameObjectsWithTag("Medium Pizza");
        GameObject[] numberOfLargePizza = GameObject.FindGameObjectsWithTag("Large Pizza");

        // Calculate each pizza size's total price
        double smallPizzaTotal = numberOfSmallPizza.Length * PlayerPrefs.GetFloat("smallPizzaPrice");
        double mediumPizzaTotal = numberOfMediumPizza.Length * PlayerPrefs.GetFloat("mediumPizzaPrice");
        double largePizzaTotal = numberOfLargePizza.Length * PlayerPrefs.GetFloat("largePizzaPrice");

        // Calculate and update sub total
        _subTotal = smallPizzaTotal + mediumPizzaTotal + largePizzaTotal;
        _subTotal = RoundToTwoDecimalPlaces(_subTotal);
    }

    /// <summary>
    /// Calculate order total by calculating text and add it and fee to subtotal
    /// </summary>
    private void CalculateTotal()
    {
        total = (_subTotal + _subTotal * _taxPercentage) + _fee;
        total = RoundToTwoDecimalPlaces(total);
    }

    /// <summary>
    /// Round a value to two decimal places if not already.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private double RoundToTwoDecimalPlaces(double value)
    {
        if (value * 100 % 1 == 0) return value;
        else return Math.Round(value, 2);
    }
}