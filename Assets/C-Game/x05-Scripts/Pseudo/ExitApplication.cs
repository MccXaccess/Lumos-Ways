using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitApplication : MonoBehaviour
{
    private IEnumerator StartTimer(float value)
    {    
        yield return new WaitForSeconds(value);
        //Application.Quit();
        Debug.Log("exit");
    }
    public void ExitApp(float timeUntilExit)
    {
        StartCoroutine(StartTimer(timeUntilExit));
    }
}