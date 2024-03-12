using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DynamicRectTransformHeight : MonoBehaviour
{
    [SerializeField] private GameObject _pizzaInTheOrder;

    private void Update()
    {
        AdjustHeight();
    }

    /// <summary>
    /// Adjust the height of the "Pizza" GameObject based on how many children it has.
    /// Each increase is a static value.
    /// </summary>
    private void AdjustHeight()
    {
        int numberOfChildren = _pizzaInTheOrder.transform.childCount;
        float height;

        if (numberOfChildren == 1)
        {
            height = 71.338f;
        }
        else if (numberOfChildren > 1)
        {
            height = numberOfChildren * 46;
        }
        else
        {
            height = 71.338f;
        }

        RectTransform recTransform = GetComponent<RectTransform>();
        recTransform.sizeDelta = new Vector2(recTransform.sizeDelta.x, height);
    }
}