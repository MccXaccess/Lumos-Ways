using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawController : MonoBehaviour
{
    public RotateObject rotateObject;
    public ModulePlatform modulePlatform;

    private void Update()
    {
        if (modulePlatform.slowDown)
        {
            for(int i = 0; i < 10; i++)
            {
                rotateObject.rotationSpeed -= Time.deltaTime * i;
            }
        }

        if (modulePlatform.targetChanged)
        {
            rotateObject.rotationSpeed = rotateObject.rotationSpeedMax;
            rotateObject.directionLeft = !rotateObject.directionLeft;
        }
    }
}