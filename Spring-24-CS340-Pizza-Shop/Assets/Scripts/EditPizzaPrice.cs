using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
public class EditPizzaPrice : MonoBehaviour
{
    //referrences here:
    // text of pizza price
    // toggles
    // input field
    public UnityEngine.UI.Toggle smallToggle;
    public UnityEngine.UI.Toggle mediumToggle;
    public UnityEngine.UI.Toggle largeToggle;
    public TMP_InputField priceInputField;

    string currentPrice = "$0.00";


    // Start is called before the first frame update
    void Start()
    {
    }

    public void UpdatePriceText()
    {
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


        priceInputField.text = currentPrice;
    }

    // Update is called once per frame


    public void ClickedToggleSmall()
    {
        if (smallToggle.isOn)
        {
            Debug.Log("Hello world");
            mediumToggle.isOn = false;
            largeToggle.isOn = false;
            UpdatePriceText();
        }
    }
    public void ClickedToggleMedium()
    {
        if (mediumToggle.isOn)
        {
            smallToggle.isOn = false;
            largeToggle.isOn = false;
            UpdatePriceText();
        }
    }
    public void ClickedToggleLarge()
    {
        if (largeToggle.isOn)
        {
            smallToggle.isOn = false;
            mediumToggle.isOn = false;
            UpdatePriceText();
        }
    }

    public void EditPrice()
    {
        currentPrice = priceInputField.text;
    }
    // update pizza price text

}




// method for eidt button to call to change pizza price
