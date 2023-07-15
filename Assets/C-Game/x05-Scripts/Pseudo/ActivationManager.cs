using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActivationManager : MonoBehaviour
{
    public void DeactivateObjects(GameObject[] a_IterableList)
    {
        IterateObjects(a_IterableList, DeactivateObject);
    }

    public void ActivateObjects(GameObject[] a_IterableList)
    {
        IterateObjects(a_IterableList, ActivateObject);
    }

    private void IterateObjects(GameObject[] a_IterableList, Action<GameObject> a_ActionMethod)
    {
        foreach (GameObject item in a_IterableList)
        {
            a_ActionMethod.Invoke(item);
        }
    }

    private void DeactivateObject(GameObject a_Object)
    {
        a_Object.SetActive(false);
    }

    private void ActivateObject(GameObject a_Object)
    {
        a_Object.SetActive(true);
    }
}
