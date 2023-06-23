using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathCheck : MonoBehaviour
{
    public AudioSource whenDies;
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Death"))
        {
            if (GameManager.Instance.IsAlive)
            {
                whenDies.Play();
                GameManager.Instance.Dead();
            }
        }
    }
}