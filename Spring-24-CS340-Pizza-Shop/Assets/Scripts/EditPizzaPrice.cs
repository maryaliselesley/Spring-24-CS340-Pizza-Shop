using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EditPizzaPrice : MonoBehaviour
{
    //referrences here:
    // text of pizza price
    // toggles
    // input field
    public UnityEngine.UI.Toggle smallToggle;
    public UnityEngine.UI.Toggle mediumToggle;
    public UnityEngine.UI.Toggle largeToggle;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
  

    public void ClickedToggleSmall()
    {
        if (smallToggle.isOn)
        {
            Debug.Log("Hello world");
            mediumToggle.isOn = false;
            largeToggle.isOn = false;
        }
    }
    public void ClickedToggleMedium()
    {
        if (mediumToggle.isOn)
        {
            smallToggle.isOn = false;
            largeToggle.isOn = false;
        }
    }
    public void ClickedToggleLarge()
    {
        if (largeToggle.isOn)
        {
            smallToggle.isOn = false;
            mediumToggle.isOn = false;
        }
    }
}
// update pizza price text





// method for eidt button to call to change pizza price
