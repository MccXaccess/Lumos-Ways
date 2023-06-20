using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathCheck : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Death"))
        {
            if (GameManager.Instance.IsAlive)
            {
                GameManager.Instance.Dead();
            }
        }
    }
}