using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 100f; 
    public float rotationSpeedMax = 100f;
    public float rotationDegreesLimit = 360f;
    
    [Tooltip("if false rotates right")] public bool directionLeft;

    void Update()
    {
        if (directionLeft)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            return;
        }
        
        transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}