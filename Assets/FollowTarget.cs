using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public bool xAxisEnabled;
    public bool yAxisEnabled;

    public Vector2 offsetValue = Vector2.zero;

    private float xAxisValue; 
    private float yAxisValue;

    private void Update()
    {
        xAxisValue = xAxisEnabled ? xAxisValue = target.transform.position.x : xAxisValue = 0.0F;
        yAxisValue = yAxisEnabled ? yAxisValue = target.transform.position.y : yAxisValue = 0.0F;


        transform.position = new Vector2(xAxisValue + offsetValue.x, yAxisValue + offsetValue.y);
    }
}