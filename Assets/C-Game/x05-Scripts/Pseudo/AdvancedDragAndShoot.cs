using UnityEngine;

public class AdvancedDragAndShoot : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float maxDragDistance = 5f;

    private Camera mainCamera;
    public Rigidbody2D rb;
    private Vector2 initialPosition;
    private Vector2 currentDragPosition;
    private bool isDragging = false;

    private void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        initialPosition = rb.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }
        else if (Input.GetMouseButton(0))
        {
            ContinueDragging();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }
    }

    private void StartDragging()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(mousePosition, rb.position) <= maxDragDistance)
        {
            isDragging = true;
            
            currentDragPosition = rb.position;
            initialPosition = currentDragPosition;

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, currentDragPosition);
            lineRenderer.SetPosition(1, currentDragPosition);
        }
    }

    private void ContinueDragging()
    {
        if (isDragging)
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            currentDragPosition = initialPosition + (mousePosition - initialPosition).normalized * maxDragDistance;
            
            lineRenderer.SetPosition(1, currentDragPosition);
        }
    }

    private void StopDragging()
    {
        if (isDragging)
        {
            Vector2 direction = (currentDragPosition - rb.position).normalized;

            rb.velocity = direction * maxDragDistance;

            isDragging = false; 
            lineRenderer.positionCount = 0;
        }
    }
}
