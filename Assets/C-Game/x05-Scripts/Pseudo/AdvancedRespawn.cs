using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedRespawn : MonoBehaviour
{
    public Transform RespawnPoint;
    public LayerMask layerMask;

    private Rigidbody2D rigidbody2D;
    
    private bool deathInRange;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if ( rigidbody2D.velocity.y == 0 )
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(10, 10), 0.0f, layerMask);


            foreach(Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Death"))
                {
                    deathInRange = true;
                    Debug.Log(collider.name);
                    return;
                }
            }                

            RespawnPoint.position = transform.position;
        }
    }
}