using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 1f;
    public float destroyDelay = 2f;
    public float force;

    private Rigidbody2D rigidbody2D;
    private GameObject toKnock;

    private Coroutine fallCoroutine;
    private Coroutine reactivateCoroutine;

    public GameObject platform;
    private IUnparent unparent;

    private float currentTime;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Player"))
        {
            currentTime = 0;

            toKnock = collisionInfo.gameObject;
            
            while (currentTime < 1.5F)
            {
                currentTime += Time.deltaTime;
            }

            Rigidbody2D playerRigidbody = toKnock.GetComponent<Rigidbody2D>();

            Vector2 direction = -(transform.position - toKnock.transform.position).normalized;

            unparent = toKnock.GetComponent<IUnparent>();

            unparent.Unparent();

            playerRigidbody.AddForce(direction * force, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collisionInfo) 
    {
        if (collisionInfo.gameObject.CompareTag("Player"))
        {
            toKnock = null;
        }
    }
}