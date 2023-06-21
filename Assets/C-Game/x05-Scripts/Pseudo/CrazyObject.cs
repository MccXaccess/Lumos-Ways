using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyObject : MonoBehaviour
{
    public float speed;
    public float radius;

    private void Start()
    {
        StartCoroutine(RandomPlace());
    }

    IEnumerator RandomPlace()
    {
        while(true)
        {
            Vector2 randomPlace = Random.insideUnitCircle * radius;
            transform.Translate(randomPlace * speed);
            yield return new WaitForSeconds(0.25f);
        }
    }
}