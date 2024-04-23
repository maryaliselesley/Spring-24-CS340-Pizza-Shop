using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Each button will have it's own instance of this class
public class AdjustAvailability : MonoBehaviour
{
    private TMP_Text _availabilityText;
    [SerializeField] private Button _button; // pizza button that adds pizza to the list

    private enum AvailabilityState
    {
        AVAILABLE,
        OUT
    }

    private AvailabilityState _currentState = AvailabilityState.AVAILABLE;

    private void Awake()
    {
        _availabilityText = GetComponentInChildren<TMP_Text>();
    }

    /// <summary>
    /// This is called with Availability buttons for pizza.<br/>
    /// Switch between "Available" and "OUT".<br/>
    /// In the "OUT" state, pizza button will not be interactable and text will disappear.
    /// </summary>
    public void ChangeAvailabilityState()
    {
        switch (_currentState)
        {
            case AvailabilityState.AVAILABLE:
                _button.interactable = false;
                _button.GetComponentInChildren<TMP_Text>().alpha = 0f; // Set alpha to 0 to "disable" text
                _availabilityText.text = "OUT";
                _currentState = AvailabilityState.OUT;
                break;

            case AvailabilityState.OUT:
                _button.interactable = true;
                _button.GetComponentInChildren<TMP_Text>().alpha = 1f;
                _availabilityText.text = "Available";
                _currentState = AvailabilityState.AVAILABLE;
                break;
        }
    }
}