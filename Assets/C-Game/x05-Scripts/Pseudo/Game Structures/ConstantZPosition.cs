using UnityEngine;

public class ConstantZPosition : MonoBehaviour
{
    public float targetZPosition = -10; // The value you want to set as the Z-coordinate

    private void Update()
    {
        // Get the current position of the object
        Vector3 currentPosition = transform.position;

        // Set the Z-coordinate to the target value
        currentPosition.z = targetZPosition;

        // Apply the updated position back to the object
        transform.position = currentPosition;
    }
}
