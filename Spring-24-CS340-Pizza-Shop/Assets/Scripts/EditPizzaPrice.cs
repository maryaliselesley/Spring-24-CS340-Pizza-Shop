using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class EditPizzaPrice : MonoBehaviour
{
    //references here:
    // text of pizza price
    // toggles
    // input field
    public UnityEngine.UI.Toggle smallToggle;
    public UnityEngine.UI.Toggle mediumToggle;
    public UnityEngine.UI.Toggle largeToggle;
    public TMP_InputField priceInputField;

    string currentPrice = "$0.00";


    // Start
    void Start()
    {
        // Set initial price text
        priceInputField.text = currentPrice;
    }

    public void UpdatePriceText()
    {
        // Update currentPrice based on toggle state
        if (smallToggle.isOn)
        {
            currentPrice = "$10.00";
        }
        else if (mediumToggle.isOn)
        {
            currentPrice = "$15.00";
        }
        else if (largeToggle.isOn)
        {
            currentPrice = "$20.00";
        }

        // Update the price input field
        priceInputField.text = currentPrice;
    }

    public void ClickedToggleSmall()
    {
        if (smallToggle.isOn)
        {
            Debug.Log("Hello world");
            mediumToggle.isOn = false;
            largeToggle.isOn = false;
            UpdatePriceText();
            SavePrice();
        }
    }

    public void ClickedToggleMedium()
    {
        if (mediumToggle.isOn)
        {
            smallToggle.isOn = false;
            largeToggle.isOn = false;
            UpdatePriceText();
            SavePrice();
        }
    }

    public void ClickedToggleLarge()
    {
        if (largeToggle.isOn)
        {
            smallToggle.isOn = false;
            mediumToggle.isOn = false;
            UpdatePriceText();
            SavePrice();
        }
    }

    
    void SavePrice()
    {
        PlayerPrefs.SetString("PizzaPrice", currentPrice);
    }

    // Method to edit the price
    public void EditPrice()
    {
        string newPriceText = priceInputField.text;
        
       
    }
}
