using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddPizza : MonoBehaviour
{
    [SerializeField] private GameObject _pizza; // Prefab
    [SerializeField] private Transform _content;
    [SerializeField] private Scrollbar _scrollBar;

    /// <summary>
    /// This is called with Pizza buttons. <br/>
    /// Instantiate a pizza GameObject and parent it to "Content" GameObject that holds all pizza. <br/>
    /// Using tags allow additional pizza types to be added with only a tag comparison rather than creating a completely new prefab.
    /// </summary>
    public void AddPizzaToItemList()
    {
        // Instantiate a new pizza object and parent it to content
        GameObject newPizza = Instantiate(_pizza, _content);

        // Look for tags to concatenate price to text
        if (newPizza.tag == "Small Pizza")
        {
            newPizza.GetComponent<TMP_Text>().text += PlayerPrefs.GetFloat("smallPizzaPrice").ToString();
        }
        else if (newPizza.tag == "Medium Pizza")
        {
            newPizza.GetComponent<TMP_Text>().text += PlayerPrefs.GetFloat("mediumPizzaPrice").ToString();
        }
        else if (newPizza.tag == "Large Pizza")
        {
            newPizza.GetComponent<TMP_Text>().text += PlayerPrefs.GetFloat("largePizzaPrice").ToString();
        }

        _scrollBar.value = 0; // Scroll the scroll bar to the bottom
    }
}