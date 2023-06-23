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

    private void Update()
    {
        centerPoint = transform.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(centerPoint, radius, layerMask);

        isNearSurface = colliders.Length > 0;

        //BoxCollider2D col = colliders[].GetComponent<BoxCollider2D>();
        
        // if (col != null && col.usedByEffector)
        // {
        //     Debug.Log("something:");
        // }

        if (isNearSurface && colliders[0].gameObject.CompareTag("Attachable"))
        {
            transform.parent.SetParent(colliders[0].gameObject.transform);
        }
        else
        {
            transform.parent.SetParent(null);
        }

        // i think those is near surfaces are not neccessary
        if (isNearSurface && colliders[0].gameObject.CompareTag("NextStage") && !initWin)
        {
            initWin = true;
        }

        if (isNearSurface && ableToStick)
        {
            if ( !soundPlayed )
            {
                whenSticked.Play();
                soundPlayed = true;
            }

            TurnPhysicsOFF(rigidbody);
            isNearSurface = true;
        }
        else
        {
            TurnPhysicsON(rigidbody);
            isNearSurface = false;
            soundPlayed = false;
            StartCoroutine(Cooldown()); // Start the cooldown coroutin
        }
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