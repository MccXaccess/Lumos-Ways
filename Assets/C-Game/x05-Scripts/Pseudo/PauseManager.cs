using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject[] parent;

    public void SetActive(bool state)
    {
        foreach (GameObject i in parent)
        {
            i.SetActive(state);
        }
    }
}