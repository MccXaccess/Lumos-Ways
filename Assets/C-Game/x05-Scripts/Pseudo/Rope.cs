using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject[] prefabRopeSegs;
    public int numLinks = 5;

    private void Start()
    {
        GenerateRope();
    }

    private void GenerateRope()
    {
        Rigidbody2D previousBody = hook;

        for (int i = 0; i < numLinks; i++)
        {
            int index = Random.Range(0, prefabRopeSegs.Length);
            GameObject newSegment = Instantiate(prefabRopeSegs[index]);
            
            newSegment.transform.parent = transform;
            newSegment.transform.position = transform.position;

            HingeJoint2D hingeJoint2D = newSegment.GetComponent<HingeJoint2D>();
            
            hingeJoint2D.connectedBody = previousBody;

            previousBody = newSegment.GetComponent<Rigidbody2D>();
        }
    }
}