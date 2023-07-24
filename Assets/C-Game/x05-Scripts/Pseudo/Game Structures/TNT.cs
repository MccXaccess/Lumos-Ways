using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    public float explosionForce = 10f;
    public float explosionRadius = 5f;
    public ParticleSystem explosionParticle;
    public AudioSource explosionSound;

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 explosionDir = collider.transform.position - transform.position;
                rb.AddForce(explosionDir.normalized * explosionForce, ForceMode2D.Impulse);
            }

            if (rb != null && rb.gameObject.CompareTag("Player"))
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                Vector2 explosionDir = collider.transform.position - transform.position;
                rb.AddForce(explosionDir.normalized * explosionForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        Explode();
        explosionParticle.transform.position = transform.position;
        explosionParticle.Play();
        explosionSound.Play();
        // destroy the parent here
        Destroy(transform.parent.gameObject, 0);
    }
}

// using UnityEngine;

// public static class Rigidbody2DExt {

//     public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force) {
//         var explosionDir = rb.position - explosionPosition;
//         var explosionDistance = explosionDir.magnitude;

//         // Normalize without computing magnitude again
//         if (upwardsModifier == 0)
//             explosionDir /= explosionDistance;
//         else {
//             // From Rigidbody.AddExplosionForce doc:
//             // If you pass a non-zero value for the upwardsModifier parameter, the direction
//             // will be modified by subtracting that value from the Y component of the centre point.
//             explosionDir.y += upwardsModifier;
//             explosionDir.Normalize();
//         }

//         rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir, mode);
//     }
// }