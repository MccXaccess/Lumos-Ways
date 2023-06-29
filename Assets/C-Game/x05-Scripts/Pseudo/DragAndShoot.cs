using UnityEngine;
using UnityEngine.EventSystems;


public class DragAndShoot : MonoBehaviour
{
    [Header("Movement")]
    public float maxPower;
    float shootPower;
    public float gravity = 1;
    [Range(0f, 0.1f)] public float slowMotion;

    public bool shootWhileMoving = false;
    public bool forwardDraging = true;
    public bool showLineOnScreen = false;

    public AudioSource whenJump;

    Transform direction;
    Rigidbody2D rb;
    LineRenderer line;
    LineRenderer screenLine;

    // Vectors // 
    Vector2 startPosition;
    Vector2 targetPosition;
    Vector2 startMousePos;
    Vector2 currentMousePos;

    // direction of the mouse
    Vector2 dire;
    [HideInInspector] public bool canShoot = false;

    float dist;

    public StickToSurface stickToSurface;

    //public Trajectory trajectory;
    void Start()
    {
        canShoot = false;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        line = GetComponent<LineRenderer>();
        direction = transform.GetChild(0);
        screenLine = direction.GetComponent<LineRenderer>();
    }

    public void Push (Vector2 force)
	{
		rb.AddForce (force * 50, ForceMode2D.Impulse);
	}

    void Update()
    {

        if ((Input.GetMouseButtonDown(0) && rb.velocity.y == 0) || stickToSurface.onSpecificInteraction)
        {
            // if (EventSystem.current.currentSelectedGameObject) return;  //ENABLE THIS IF YOU DON'T WANT TO IGNORE UI
            // trajectory.Show();
            MouseClick();
        }

        if ((Input.GetMouseButton(0) && rb.velocity.y == 0) || stickToSurface.onSpecificInteraction)
        {
            // if (EventSystem.current.currentSelectedGameObject) return;  //ENABLE THIS IF YOU DON'T WANT TO IGNORE UI
            MouseDrag();

            //trajectory.UpdateDots(transform.position, new Vector2(dire.x / 10 * shootPower, dire.y / 10 * shootPower));

            if (shootWhileMoving) rb.velocity /= (1 + slowMotion);

        }

        if ((Input.GetMouseButtonUp(0) && rb.velocity.y == 0) || stickToSurface.onSpecificInteraction)
        {
            // if (EventSystem.current.currentSelectedGameObject) return;  //ENABLE THIS IF YOU DON'T WANT TO IGNORE UI
            MouseRelease();
            //trajectory.Hide ();
        }

        if (shootWhileMoving)
            return;

        if (rb.velocity.magnitude == 0)
        {
            //rb.velocity = new Vector2(0, 0); //ENABLE THIS IF YOU WANT THE BALL TO STOP IF IT'S MOVING SO SLOW
            canShoot = true;
        }
    }


    // MOUSE INPUTS
    void MouseClick()
    {
        if (shootWhileMoving)
        {
            Vector2 dir = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.right = dir * 1;

            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return;
        }

        if (canShoot)
        {
            Vector2 dir = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.right = dir * 1;

            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void MouseDrag()
    {
        if (shootWhileMoving)
        {
            LookAtShootDirection();
            DrawLine();

            if (showLineOnScreen)
                DrawScreenLine();

            float distance = Vector2.Distance(currentMousePos, startMousePos);

            if (distance > 1)
            {
                line.enabled = true;

                if (showLineOnScreen)
                    screenLine.enabled = true;
            }

            return;
        }

        if (canShoot)
        {
            LookAtShootDirection();
            DrawLine();

            if (showLineOnScreen)
                DrawScreenLine();

            float distance = Vector2.Distance(currentMousePos, startMousePos);

            if (distance > 1)
            {
                line.enabled = true;

                if (showLineOnScreen)
                    screenLine.enabled = true;
            }
        }
    }

    void MouseRelease()
    {
        stickToSurface.TurnPhysicsON();
        StickToSurface.InitTakeoff();

        if (shootWhileMoving ) // !EventSystem.current.IsPointerOverGameObject())
        {
            Shoot();
            screenLine.enabled = false;
            line.enabled = false;
            return;
        }
        
        if (canShoot) // !EventSystem.current.IsPointerOverGameObject())
        {
            Shoot();
            screenLine.enabled = false;
            line.enabled = false;
        }
    }


    // ACTIONS  
    void LookAtShootDirection()
    {
        Vector3 dir = startMousePos - currentMousePos;

        if (forwardDraging)
        {
            transform.right = dir * -1;
        }
        else
        {
            transform.right = dir;
        }


        float dis = Vector2.Distance(startMousePos, currentMousePos);
        dis *= 4;


        if (dis < maxPower)
        {
            direction.localPosition = new Vector2(dis / 6, 0);
            shootPower = dis;
        }
        else
        {
            shootPower = maxPower;
            direction.localPosition = new Vector2(maxPower / 6, 0);
        }

    }
    public void Shoot()
    {
        canShoot = false;
        whenJump.Play();
        stickToSurface.WhenShooted();
        // rb.AddForce(new Vector2(5f, 10f), ForceMode2D.Impulse);
        rb.velocity = transform.right * shootPower;
    }


    void DrawScreenLine()
    {

        screenLine.positionCount = 1;
        screenLine.SetPosition(0, startMousePos);

        currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        dire = startMousePos - currentMousePos;
        // NOTE: DON'T FORGET TO CHANGE 5 VALUE TO SHOOT POWER VALUE?
        dist = Mathf.Clamp(Vector2.Distance(startMousePos, currentMousePos), 0, 5);
    
        //currentMousePos = startMousePos + (currentMousePos.normalized * dist);
        
        Vector2 rotatedDire = new Vector2(-dire.x, -dire.y);

        currentMousePos = startMousePos + (rotatedDire.normalized * dist);

        screenLine.positionCount = 2;
        screenLine.SetPosition(1, currentMousePos);
    }

    void DrawLine()
    {

        startPosition = transform.position;

        line.positionCount = 1;
        line.SetPosition(0, startPosition);

        targetPosition = direction.position;

        line.positionCount = 2;
        line.SetPosition(1, targetPosition);
    }
}