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

    
    

    // temporary variables
    public static bool isNearSurface; 
    public static bool ableToStick;

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
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

private void Update()
{
    centerPoint = transform.position;

    Collider2D[] colliders = Physics2D.OverlapCircleAll(centerPoint, radius, layerMask);

    isNearSurface = colliders.Length > 0;

    if (isNearSurface && ableToStick)
    {
        TurnPhysicsOFF(rigidbody);
        isNearSurface = true;
    }
    else
    {
        TurnPhysicsON(rigidbody);
        isNearSurface = false;
        StartCoroutine(Cooldown()); // Start the cooldown coroutin
    }
}


    // private void OnCollisionEnter2D(Collision2D other) 
    // {
    //     StopCoroutine(timer());
    //     // we did hit the surface ( aka grounded )
    //     if (other != null && lastTimeTouched >= lastTimeFixed)
    //     {
    //         TurnPhysicsOFF(rigidbody);
    //         isNearSurface = true;
    //     }
    //     else
    //     {
    //         TurnPhysicsON(rigidbody);
    //         isNearSurface = false;
    //     }
    // }

    // private void OnCollisionStay2D(Collision2D other) 
    // {
        
    // }

    // private void OnCollisionExit2D(Collision2D other) 
    // {
    //     StartCoroutine(timer());
    // }

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