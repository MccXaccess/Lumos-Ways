using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulePlatform : MonoBehaviour
{
    private Rigidbody rigidbody;

    public List<Transform> targets = new List<Transform>();

    private Transform currentTarget;

    public float speed;


    private void Update()
    {
        if (currentTarget == null)
        {
            currentTarget = targets[0];
        }

        float distance = Vector2.Distance(transform.position, currentTarget.position);

        if (distance > 0.2F)
        {
            Vector2 direction = currentTarget.position - transform.position;
            Vector2 movement = direction.normalized * speed * Time.deltaTime;
            transform.Translate(movement);
            return;
        }
        ChangeTarget();
    }

    private void ChangeTarget()
    {
        int currentIndex = targets.IndexOf(currentTarget);

        if (currentIndex == targets.Count - 1)
        {
            currentTarget = targets[0];
            return;
        }

        currentTarget = targets[currentIndex + 1];
    }
}