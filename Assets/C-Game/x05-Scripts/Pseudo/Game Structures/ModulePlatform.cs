using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulePlatform : MonoBehaviour
{

    public List<Transform> targets = new List<Transform>();

    private Transform currentTarget;

    [HideInInspector] public bool targetChanged;
    [HideInInspector] public bool slowDown;

    public float speed;

    private void Update()
    {
        if (currentTarget == null)
        {
            currentTarget = targets[0];
        }

        float distance = Vector2.Distance(transform.position, currentTarget.position);

        if (distance < 6F)
        {
            slowDown = true;
        }

        if (distance > 0.2F)
        {
            targetChanged = false;
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

        slowDown = false;

        if (currentIndex == targets.Count - 1)
        {
            targetChanged = true;
            currentTarget = targets[0];
            return;
        }

        currentTarget = targets[currentIndex + 1];
        targetChanged = true;
    }
}