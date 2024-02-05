using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddPizza : MonoBehaviour
{
    // prefabs
    [SerializeField] private GameObject _pizza;

    // prefabs will be added to "Content" object, which auto manages masking and scrolling
    [SerializeField] private Transform _content;

    public void AddPizzaToItemList()
    {
        // instantiate a new gameobject so the prefab content doesn't get changed
        GameObject newPizza = Instantiate(_pizza, _content);

        // look for tags to concatenate price to text
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
    }
}