using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEvent : MonoBehaviour
{
    [SerializeField] string m_CompareTag;

    public delegate void OnCollision();
    public event OnCollision OnCollisionEnterEvent;

    private void OnCollisionEnter(Collision a_CollisionInfo) 
    {
        if (a_CollisionInfo.gameObject.CompareTag(m_CompareTag))
        {
            if (OnCollisionEnterEvent != null)
            {
                OnCollisionEnterEvent.Invoke();
            }
        }
    }
}