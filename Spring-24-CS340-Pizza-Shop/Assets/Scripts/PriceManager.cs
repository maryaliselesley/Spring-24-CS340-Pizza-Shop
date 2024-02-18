using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PriceManager : MonoBehaviour
{
    public static PriceManager instance;

    // pizza prices
    // TODO: change pizza default prices
    private double _smallPizzaPrice;
    private double _mediumPizzaPrice;
    private double _largePizzaPrice;

    // fees
    private const double _deliveryFee = 3;
    private const double _dineInServiceFee = 0.05;

    // order prices
    // TODO: edit text percentage
    private static double _fee = 0;
    private static double _taxPercentage = 0.1f;
    private static double _subTotal = 0;
    public static double total = 0;

    [SerializeField] private TMP_Text _subTotalText;
    [SerializeField] private TMP_Text _totalText;
    [SerializeField] private TMP_Text _feeText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _smallPizzaPrice = PlayerPrefs.GetFloat("smallPizzaPrice", 4.99f);
        _mediumPizzaPrice = PlayerPrefs.GetFloat("mediumPizzaPrice", 6.99f);
        _largePizzaPrice = PlayerPrefs.GetFloat("largePizzaPrice", 9.99f);
    }

    // ideally, prices gets updated only when a pizza is added or removed
    // but due to the order of execution, pizza counts do not always update correctly when pizzas are being removed
    public void Update()
    {
        // get total number of each type of pizza using tags
        GameObject[] numberOfSmallPizza = GameObject.FindGameObjectsWithTag("Small Pizza");
        GameObject[] numberOfMediumPizza = GameObject.FindGameObjectsWithTag("Medium Pizza");
        GameObject[] numberOfLargePizza = GameObject.FindGameObjectsWithTag("Large Pizza");

        // calculate each pizza's total price
        double smallPizzaTotal = numberOfSmallPizza.Length * _smallPizzaPrice;
        double mediumPizzaTotal = numberOfMediumPizza.Length * _mediumPizzaPrice;
        double largePizzaTotal = numberOfLargePizza.Length * _largePizzaPrice;

        // calculate and update sub total
        _subTotal = smallPizzaTotal + mediumPizzaTotal + largePizzaTotal;
        _subTotal = RoundToTwoDecimalPlaces(_subTotal);
        _subTotalText.text = "Subtotal: $" + _subTotal.ToString();

        // calculate and update fee based on the scene (order type)
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

        // calculate and update total
        total = (_subTotal + _subTotal * _taxPercentage) + _fee;
        total = RoundToTwoDecimalPlaces(total);
        _totalText.text = "Total: $" + total.ToString();
    }

    private double RoundToTwoDecimalPlaces(double value)
    {
        if (value * 100 % 1 == 0)
        {
            return value;
        }
        else
        {
            return Math.Round(value, 2);
        }
    }
}