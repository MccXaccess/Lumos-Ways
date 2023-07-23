using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandlerPickedItem : MonoBehaviour
{
    private OnCollisionEvent onCollision;

    private void Start()
    {
        onCollision = GetComponent<OnCollisionEvent>();
        
        onCollision.OnCollisionEnterEvent += HandlePickup;
    }

    private void OnDisable() 
    {
        onCollision.OnCollisionEnterEvent -= HandlePickup;
    }

    private void HandlePickup()
    {
        Debug.Log($"Picked Item! {gameObject.name}");
        Destroy(gameObject);
    }
}