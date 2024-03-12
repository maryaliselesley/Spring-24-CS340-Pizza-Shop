using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePizza : MonoBehaviour
{
    public void DeletePizzaFromList()
    {
        Destroy(transform.parent.gameObject);
    }
}