using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePizza : MonoBehaviour
{
    /// <summary>
    /// This is called by the "x" button attached to every order game object on item list
    /// </summary>
    public void DeletePizzaFromList()
    {
        Destroy(transform.parent.gameObject);
    }
}