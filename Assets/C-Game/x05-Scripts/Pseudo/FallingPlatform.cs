using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 1f;
    public float destroyDelay = 2f;
    public float force;

    private Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall(collisionInfo.gameObject, collisionInfo));
        }
    }

    IEnumerator Fall(GameObject go, Collision2D collision)
    {
        yield return new WaitForSeconds(fallDelay);
        
        Rigidbody2D playerRigidbody = go.GetComponent<Rigidbody2D>();

        playerRigidbody.bodyType = RigidbodyType2D.Dynamic;

        Vector2 direction = -(transform.position - go.transform.position).normalized;

        playerRigidbody.AddForce(direction * force, ForceMode2D.Impulse);

        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        Destroy(gameObject, destroyDelay);  
    }
}