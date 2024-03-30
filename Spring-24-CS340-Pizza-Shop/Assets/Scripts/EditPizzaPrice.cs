using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditPizzaPrice : MonoBehaviour
{
    public Toggle smallToggle;
    public Toggle mediumToggle;
    public Toggle largeToggle;
    public TextMeshProUGUI smallPriceText; 
    public TextMeshProUGUI mediumPriceText;
    public TextMeshProUGUI largePriceText; 
    public TMP_InputField priceInputField; 
    public TextMeshProUGUI errorMessageText; 

    
    private string smallPrice = "$10.00";
    private string mediumPrice = "$15.00";
    private string largePrice = "$20.00";

    // Start
    void Start()
    {
       
        smallPriceText.text = smallPrice;
        mediumPriceText.text = mediumPrice;
        largePriceText.text = largePrice;
    }

    public void UpdatePriceText()
    {
        // Update the price text based on toggle state
        if (smallToggle.isOn)
        {
            smallPriceText.text = smallPrice;
        }
        else
        {
            smallPriceText.text = "";
        }

        if (mediumToggle.isOn)
        {
            mediumPriceText.text = mediumPrice;
        }
        else
        {
            mediumPriceText.text = "";
        }

        if (largeToggle.isOn)
        {
            largePriceText.text = largePrice;
        }
        else
        {
            largePriceText.text = "";
        }
    }

    public void ClickedToggleSmall()
    {
        if (smallToggle.isOn)
        {
            mediumToggle.isOn = false;
            largeToggle.isOn = false;
            UpdatePriceText();
            SavePrice(smallPrice); 
        }
    }

    public void ClickedToggleMedium()
    {
        if (mediumToggle.isOn)
        {
            smallToggle.isOn = false;
            largeToggle.isOn = false;
            UpdatePriceText();
            SavePrice(mediumPrice); 
        }
    }

    public void ClickedToggleLarge()
    {
        if (largeToggle.isOn)
        {
            smallToggle.isOn = false;
            mediumToggle.isOn = false;
            UpdatePriceText();
            SavePrice(largePrice); 
        }
    }

    void SavePrice(string price)
    {
        // Save the selected pizza price
        PlayerPrefs.SetString("PizzaPrice", price);
    }

    // Method to edit the price
    public void EditPrice()
    {
        
        string newPriceText = priceInputField.text;

        
        if (IsValidPriceInput(newPriceText))
        {
            
            if (smallToggle.isOn)
            {
                smallPrice = newPriceText;
                smallPriceText.text = smallPrice;
            }
            else if (mediumToggle.isOn)
            {
                mediumPrice = newPriceText;
                mediumPriceText.text = mediumPrice;
            }
            else if (largeToggle.isOn)
            {
                largePrice = newPriceText;
                largePriceText.text = largePrice;
            }

            
            ClearErrorMessage();
        }
        else
        {
            
            DisplayErrorMessage("Invalid price input. Please enter a valid price with up to two decimal places, optionally starting with a '$' symbol.");
        }
    }

    // Method to validate price input
    private bool IsValidPriceInput(string input)
    {
       
        string pattern = @"^\$?(\d+(\.\d{0,2})?)?$";
        return System.Text.RegularExpressions.Regex.IsMatch(input, pattern);
    }

    // Method to display error message
    private void DisplayErrorMessage(string message)
    {
        
        errorMessageText.text = message;

        // Clear the input field for another attempt
        priceInputField.text = "";
    }

    // Method to clear error message
    private void ClearErrorMessage()
    {
        // Clear error message in UI
        errorMessageText.text = "";
    }
}
