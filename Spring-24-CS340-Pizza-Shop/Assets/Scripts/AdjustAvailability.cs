using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdjustAvailability : MonoBehaviour
{
    private TMP_Text _availabilityText;
    [SerializeField] private Button _button; // pizza button that adds pizza to the list

    private void Awake()
    {
        _availabilityText = GetComponentInChildren<TMP_Text>();
    }

    public void ChangeAvailabilityState()
    {
        // swap between available and out state
        if (_availabilityText.text.Equals("Available"))
        {
            _button.interactable = false; // disable button interaction
            _button.GetComponentInChildren<TMP_Text>().alpha = 0f; // set alpha to 0 to "disable" text
            _availabilityText.text = "OUT";
        }
        else if (_availabilityText.text.Equals("OUT"))
        {
            _button.interactable = true;
            _button.GetComponentInChildren<TMP_Text>().alpha = 1f;
            _availabilityText.text = "Available";
        }
    }
}