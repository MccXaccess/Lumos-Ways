using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndShoot : MonoBehaviour
{
    [Header("Movement")]
    public float maxPower;
    float shootPower;
    public float gravity = 1;

    [Space(10)]
    public bool forwardDraging = true;
    public bool showLineOnScreen = false;
    public bool canShoot = false;

    [Space(10)]
    public AudioSource whenJump;

    [Space(10)]
    public Transform directionTransform;
    private Rigidbody2D rb2d;
    
    [Space(10)]
    public LineRenderer dragPlayerLine;
    public LineRenderer dragMouseLine;

    Vector2 startPosition;
    Vector2 targetPosition;
    
    Vector2 startMousePos;
    Vector2 currentMousePos;

    Vector2 startMousePosScreenSpace;

    [Space(10)]
    public StickToSurface stickToSurface;

    private void Start()
    {
        canShoot = false;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = gravity;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && rb2d.velocity.y == 0 && canShoot)
        {
            MouseClick();
        }

        if (Input.GetMouseButton(0) && rb2d.velocity.y == 0 && canShoot)
        {
            MouseDrag();
        }

        if (Input.GetMouseButtonUp(0) && rb2d.velocity.y == 0 && canShoot)
        {
            MouseRelease();
        }

        if (rb2d.velocity.magnitude == 0)
        {
            canShoot = true;
        }
    }

    private void LateUpdate()
    {
        if (showLineOnScreen)
            DrawMouseLine();    
    }

    private void MouseClick()
    {
        startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        startMousePosScreenSpace = Input.mousePosition;

        Vector2 direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        transform.right = direction * 1;
    }

    private void MouseDrag()
    {
        LookAtShootDirection();
        CalculateShootPower();
        DrawPlayerLine();

        startMousePos = Camera.main.ScreenToWorldPoint(startMousePosScreenSpace);
        
        currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float distance = Vector2.Distance(currentMousePos, startMousePos);

        if (distance > 1)
        {
            dragPlayerLine.enabled = true;

            if (showLineOnScreen)
                dragMouseLine.enabled = true;
        }
    }

    private void MouseRelease()
    {
        stickToSurface.TurnPhysicsON();
        StickToSurface.InitTakeoff();
        
        if (canShoot)
        {
            Shoot();
            dragMouseLine.enabled = false;
            dragPlayerLine.enabled = false;
        }
    }

    private void LookAtShootDirection()
    {
        Vector3 direction = startMousePos - currentMousePos;

        transform.right = forwardDraging ? transform.right = direction * -1 : transform.right = direction;
    }

    private void CalculateShootPower()
    {
        float distance = Vector2.Distance(startMousePos, currentMousePos);
        distance *= 4;

        if (distance < maxPower)
        {
            directionTransform.localPosition = new Vector2(distance / 6, 0);
            shootPower = distance;
        }
        else
        {
            directionTransform.localPosition = new Vector2(maxPower / 6, 0);
            shootPower = maxPower;
        }
    }

    private void Shoot()
    {
        canShoot = false;
        whenJump.Play();
        stickToSurface.WhenShooted();
        rb2d.velocity = transform.right * shootPower;
    }

    private void DrawMouseLine()
    {
        dragMouseLine.positionCount = 1;
        dragMouseLine.SetPosition(0, startMousePos);

        Vector2 direction = startMousePos - currentMousePos;

        // NOTE: DON'T FORGET TO CHANGE 5 VALUE TO SHOOT POWER VALUE?
        float distance = Mathf.Clamp(Vector2.Distance(startMousePos, currentMousePos), 0, 5);
        
        Vector2 rotatedDirection = new Vector2(-direction.x, -direction.y);

        currentMousePos = startMousePos + (rotatedDirection.normalized * distance);

        dragMouseLine.positionCount = 2;
        dragMouseLine.SetPosition(1, currentMousePos);
    }

    private void DrawPlayerLine()
    {
        startPosition = transform.transform.position;

        dragPlayerLine.positionCount = 1;
        dragPlayerLine.SetPosition(0, startPosition);

        targetPosition = directionTransform.position;

        dragPlayerLine.positionCount = 2;
        dragPlayerLine.SetPosition(1, targetPosition);
    }
}