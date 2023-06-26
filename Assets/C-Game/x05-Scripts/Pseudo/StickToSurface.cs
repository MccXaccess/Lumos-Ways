using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToSurface : MonoBehaviour
{
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

    // temporary variables
    public static bool isNearSurface; 
    public static bool ableToStick;

    private bool soundPlayed;
    
    [HideInInspector] public bool isSqueezed = false;
    public float raycastDistance = 0.1f;

    private float lastTimeJumped;
    public float TimeJumpAmount;


    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        layerMaskValue = layerMask.value;
    }

    private IEnumerator Cooldown()
    {
        float currentTime = 0.0f;

        while (currentTime < lastTimeFixed)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        lastTimeTouched = currentTime;
        ableToStick = true; // Set ableToStick to true after the cooldown
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        transform.parent.SetParent(colliders[0].gameObject.transform);
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
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

        isNearSurface = colliders.Length > 0;

        if (isNearSurface ) //&& colliders[0].gameObject.CompareTag("Attachable"))
        {
            //TurnPhysicsOFF(rigidbody);
            transform.parent.SetParent(colliders[0].gameObject.transform);
            //isNearSurface = true;
        }
        else
        {
            //TurnPhysicsON(rigidbody);
            transform.parent.SetParent(null);
            soundPlayed = false;
            //isNearSurface = false;
        }

        // i think those is near surfaces are not neccessary
        if (isNearSurface && colliders[0].gameObject.CompareTag("NextStage") && !initWin)
        {
            initWin = true;
        }

        if (rigidbody.velocity.y == 0 && rigidbody.velocity.x == 0 && !soundPlayed)
        {
            whenSticked.Play();
            soundPlayed = true;
        }

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
    public static void TurnPhysicsON(Rigidbody2D rb)
    {    
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public static void TurnPhysicsOFF(Rigidbody2D rb)
    {
        rb.bodyType = RigidbodyType2D.Static;
    }
}