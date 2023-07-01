using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 5f;
    private bool collidedWithPlayer;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if ( collidedWithPlayer )
        {
            Destroy(gameObject, 0.25f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collidedWithPlayer = true;
            return;
        }

        Destroy(gameObject);
    }
}