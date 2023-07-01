using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyObject : MonoBehaviour
{
    public float teleportRadius = 5f;
    public float intervalTime = 0.5f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        StartCoroutine(TeleportRandomly());
    }

    IEnumerator TeleportRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalTime);
            Vector2 randomPosition = Random.insideUnitCircle.normalized * teleportRadius;
            transform.position = initialPosition + new Vector3(randomPosition.x, randomPosition.y, 0);
        }
    }
}