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

    private float smallPizzaPrice;
    private float mediumPizzaPrice;
    private float largePizzaPrice;

    private bool inputEdited; 

    
    void Start()
    {
        
        smallPizzaPrice = PlayerPrefs.GetFloat("smallPizzaPrice");
        mediumPizzaPrice = PlayerPrefs.GetFloat("mediumPizzaPrice");
        largePizzaPrice = PlayerPrefs.GetFloat("largePizzaPrice");

        
        UpdateDisplayedPrices();

        
        priceInputField.contentType = TMP_InputField.ContentType.DecimalNumber;
        priceInputField.characterValidation = TMP_InputField.CharacterValidation.Decimal;
    }

    
    public void ClickedToggleSmall()
    {
        UpdateToggleState(smallToggle);
    }

    
    public void ClickedToggleMedium()
    {
        UpdateToggleState(mediumToggle);
    }

    
    public void ClickedToggleLarge()
    {
        UpdateToggleState(largeToggle);
    }

   
    private void UpdateToggleState(Toggle toggle)
    {
        if (toggle.isOn)
        {
            smallToggle.isOn = toggle == smallToggle;
            mediumToggle.isOn = toggle == mediumToggle;
            largeToggle.isOn = toggle == largeToggle;
        }
    }

    // Method to edit the price
    public void EditPrice()
    {
        string newPriceText = priceInputField.text.Trim();

        if (IsValidPriceInput(newPriceText))
        {
            inputEdited = true; 
            ClearErrorMessage();
        }
        else
        {
            DisplayErrorMessage("Invalid price input. Please enter a valid price.");
        }
    }

    // Update prices when "Edit" button is clicked
    public void UpdatePrices()
    {
        if (inputEdited)
        {
            float newPrice = float.Parse(priceInputField.text.Trim());

            if (smallToggle.isOn)
            {
                smallPizzaPrice = newPrice;
            }
            else if (mediumToggle.isOn)
            {
                mediumPizzaPrice = newPrice;
            }
            else if (largeToggle.isOn)
            {
                largePizzaPrice = newPrice;
            }

            
            PlayerPrefs.SetFloat("smallPizzaPrice", smallPizzaPrice);
            PlayerPrefs.SetFloat("mediumPizzaPrice", mediumPizzaPrice);
            PlayerPrefs.SetFloat("largePizzaPrice", largePizzaPrice);

           
            UpdateDisplayedPrices();

            
            inputEdited = false;
        }
    }

    // Validate price input
    private bool IsValidPriceInput(string input)
    {
        string pattern = @"^\d+(\.\d{0,2})?$";
        return System.Text.RegularExpressions.Regex.IsMatch(input, pattern);
    }

    // Display error message
    private void DisplayErrorMessage(string message)
    {
        errorMessageText.text = message;
        priceInputField.text = ""; // Clear input field for another attempt
    }

    // Clear error message
    private void ClearErrorMessage()
    {
        errorMessageText.text = ""; // Clear error message in UI
    }

    // Format price with two decimal places and a dollar sign
    private string FormatPrice(float price)
    {
        return "$" + price.ToString("F2");
    }

    // Update the displayed prices under toggles
    private void UpdateDisplayedPrices()
    {
        
        smallPriceText.text = FormatPrice(smallPizzaPrice);
        mediumPriceText.text = FormatPrice(mediumPizzaPrice);
        largePriceText.text = FormatPrice(largePizzaPrice);
    }
}
