using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedRespawn : MonoBehaviour
{
    public Transform RespawnPoint;

    private Rigidbody2D rigidbody2D;

    private float timeElapsed;
    public float maxTime;

    private Vector3 currentPos;
    private Vector3 previousPos;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        previousPos = transform.position;

        if ( rigidbody2D.velocity.y == 0 && currentPos == previousPos )
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > maxTime)
            {
                RespawnPoint.position = transform.position;
                timeElapsed = 0;
            }
        }

        currentPos = transform.position;
    }
}