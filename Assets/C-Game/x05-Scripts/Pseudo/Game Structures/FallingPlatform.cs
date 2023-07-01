using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 1f;
    public float destroyDelay = 2f;
    public float force;

    private Rigidbody2D rigidbody2D;

    private GameObject toKnock;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Player"))
        {
            toKnock = collisionInfo.gameObject;
            StartCoroutine(Fall());
        }
    }

    private void OnCollisionExit2D(Collision2D collisionInfo) 
    {
        if (collisionInfo.gameObject.CompareTag("Player"))
        {
            toKnock = null;
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);

        if (toKnock != null)
        {
            Rigidbody2D playerRigidbody = toKnock.GetComponent<Rigidbody2D>();

            Vector2 direction = -(transform.position - toKnock.transform.position).normalized;

            playerRigidbody.bodyType = RigidbodyType2D.Dynamic;

            playerRigidbody.AddForce(direction * force, ForceMode2D.Impulse);
        }

        StartCoroutine(Appear());

        //rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        
        //Destroy(this.gameObject, destroyDelay);  
    }

    IEnumerator Appear()
    {
        gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        gameObject.SetActive(true);
    }
}