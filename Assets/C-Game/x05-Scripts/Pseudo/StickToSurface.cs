using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToSurface : MonoBehaviour
{
    private DragAndShoot dragAndShoot;

    public float radius = 2.5f;
    public Vector2 centerPoint;
    [SerializeField] private LayerMask layerMask;
    private Rigidbody2D rigidbody;

    // will use it to define strict time to check for passing
    public float lastTimeFixed;
    // data that will be modified troghout the timer logic
    float lastTimeTouched;

    int layerMaskValue;

    public bool initWin;

    public AudioSource whenSticked;

    public bool onSpecificInteraction;

    public bool ableToPutRespawn;

    // temporary variables
    // ! this was set to true ( is near surface ) becuase the first hit was always late due to false at start (recommended to put almost everthing to true at init )
    // ! to avoid any unnecessary problems
    // ~ when you will refactor the code i know you will start from here ( i guess ) and then you will need to include almost everything because it's everythnign for game
    public static bool isNearSurface = true; 
    public static bool ableToStick = true;

    private bool soundPlayed;
    
    [HideInInspector] public bool isSqueezed = false;
    public float raycastDistance = 0.1f;

    private float lastTimeJumped;
    public float TimeJumpAmount;


    private void Start()
    {
        dragAndShoot = GetComponent<DragAndShoot>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        layerMaskValue = layerMask.value;
    }

    private IEnumerator Cooldown()
    {
        float currentTime = 0.0f;

        while (currentTime < lastTimeFixed)
        {
            ableToStick = false;
            currentTime += Time.deltaTime;
            yield return null;
        }

        lastTimeTouched = currentTime;
        ableToStick = true;
    }

    public void WhenShooted()
    {
        StartCoroutine(Cooldown());
        onSpecificInteraction = false;
        isNearSurface = false;
        TurnPhysicsON(rigidbody);
        transform.parent.SetParent(null);
        soundPlayed = false;
    }

    private void Update()
    {
        centerPoint = transform.position;

        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.up, raycastDistance, layerMask);

        if (hit1.collider != null && hit2.collider != null)
        {
            isSqueezed = true;
        }
        else
        {
            isSqueezed = false;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(centerPoint, radius, layerMask);

        // if (ableToStick && colliders != null)
        // {
        //     TurnPhysicsOFF(rigidbody);
            
        //     transform.parent.SetParent(colliders[0].gameObject.transform);
        // }

        isNearSurface = colliders.Length > 0;

        if (isNearSurface && colliders[0]?.CompareTag("Death") == true)
        {
            if (GameManager.Instance.IsAlive)
            {
                GameManager.Instance.Dead();
                return;
            }
        }

        if (isNearSurface && colliders[0]?.CompareTag("NextStage") == true && !initWin)
        {
            initWin = true;
            colliders[0].gameObject.SetActive(false);
            GameManager.Instance.InitWin();
            initWin = false;
            return;
        }

        if (isNearSurface && ableToStick && colliders[0]?.CompareTag("No Spawn Zone") == true)
        {
            if (!soundPlayed)
            {
                whenSticked.Play();
                soundPlayed = true;
            }

            TurnPhysicsOFF(rigidbody);

            ableToPutRespawn = false;
            return;
        }

        // !YOU CAN SKIP DOING CHECK FOR NULL BECAUSE IT DOESNT MAKE SENSE BECUASE ISNEARSURFACE IS ALREADY DOING IT. SHIT
        if (isNearSurface && colliders[0]?.CompareTag("Ignore But Cannot Shoot") == true)
        {
            soundPlayed = false;
            return;
        }

        if (isNearSurface && colliders[0]?.CompareTag("Ignore But Can Shoot") == true)
        {
            soundPlayed = false;
            dragAndShoot.canShoot = true;
            onSpecificInteraction = true;
            return;
        }

        if (isNearSurface && ableToStick)
        {
            if (!soundPlayed)
            {
                whenSticked.Play();
                soundPlayed = true;
            }

            ableToPutRespawn = true;
            TurnPhysicsOFF(rigidbody);
            transform.parent.SetParent(colliders[0].gameObject.transform);
        }

        //else
        //{
        //     //TurnPhysicsON(rigidbody);
        //     transform.parent.SetParent(null);
        //     soundPlayed = false;
        //     //isNearSurface = false;
        // }

        // i think those is near surfaces are not neccessary

        // if (isNearSurface && ableToStick)
        // {
        //     TurnPhysicsOFF(rigidbody);
        //     isNearSurface = true;
        // }

        // else
        // {
        //     soundPlayed = false;
        //     TurnPhysicsON(rigidbody);
        //     isNearSurface = false;
            
        //     StartCoroutine(Cooldown()); // Start the cooldown coroutin
        // }
    }

    public static void InitTakeoff()
    {
        ableToStick = false; // Set ableToStick to false when turning off physics
    }
    public void TurnPhysicsON(Rigidbody2D rb)
    {    
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void TurnPhysicsON()
    {    
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    public void TurnPhysicsOFF(Rigidbody2D rb)
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void TurnPhysicsOFF()
    {
        rigidbody.bodyType = RigidbodyType2D.Static;
    }
}