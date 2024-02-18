using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditCardInputToggle : MonoBehaviour
{
    private Toggle _toggle;
    [SerializeField] private GameObject _creditCardInputFields;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void Update()
    {
        // show credit card input fields based on toggle state
        if (_toggle.isOn)
        {
            _creditCardInputFields.SetActive(true);
        }
        else if (!_toggle.isOn)
        {
            _creditCardInputFields.SetActive(false);
        }
    }
}