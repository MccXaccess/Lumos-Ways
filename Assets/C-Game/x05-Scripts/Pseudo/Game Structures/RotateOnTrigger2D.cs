using UnityEngine;

public class RotateOnOverlapBox2D : MonoBehaviour
{
    public bool rotateLeft = true; // Set this to true if you want the object to rotate left on overlap, false for right
    public float rotationAmount = 90f; // Amount of degrees to rotate
    public float rotationSpeed = 5f; // Speed of rotation
    public Vector2 boxSize = new Vector2(1f, 1f); // Size of the overlap box

    private Quaternion initialRotation;
    private bool rotating = false;

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (rotating)
        {
            Rotate();
        }
    }

    private void FixedUpdate()
    {
        // Check for overlaps in the 2D box area
        Collider2D[] overlaps = Physics2D.OverlapBoxAll(transform.position, boxSize, 0f);

        // Check if the player is in the overlaps
        foreach (Collider2D overlap in overlaps)
        {
            if (overlap.CompareTag("Player"))
            {
                rotating = true;
                return;
            }
        }

        // If player is not in the overlaps, stop rotating
        rotating = false;
        ReturnToInitialRotation();
    }

    private void Rotate()
    {
        float targetRotation = rotateLeft ? -rotationAmount : rotationAmount;
        Quaternion targetQuaternion = initialRotation * Quaternion.Euler(0, 0, targetRotation);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetQuaternion, rotationSpeed * Time.deltaTime);
    }

    private void ReturnToInitialRotation()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
