using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinematicCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float maxOrthoSize;
    public float defaultSize;
    public float minOrthoSize;

    private bool increaseStarted;
    private bool decreaseStarted;

    [Tooltip("assign players rb to work with it's velocity")] public Rigidbody2D target;

    private void Start()
    {
        defaultSize = virtualCamera.m_Lens.OrthographicSize;
    }

    private void Update()
    {
        if (target.velocity.y < -10f && !increaseStarted)
        {
            StopAllCoroutines();
            StartCoroutine(IncreaseOrtho());
            increaseStarted = true;
            decreaseStarted = false;
        }

        if (target.velocity.y == 0f && !decreaseStarted)
        {
            StopAllCoroutines();
            StartCoroutine(DecreaseOrtho());
            increaseStarted = false;
            decreaseStarted = true;
        }
    }

    private IEnumerator IncreaseOrtho()
    {
        float startTime = 0.0f;
        float endTime = maxOrthoSize - defaultSize;

        while (startTime < endTime)
        {
            startTime += Time.deltaTime;
            //Debug.Log($"from Increase startTime - {startTime} : endTime - {endTime}");
            virtualCamera.m_Lens.OrthographicSize = defaultSize + startTime;

            yield return null;
        }
    }

    private IEnumerator DecreaseOrtho()
    {
        float startTime = maxOrthoSize - virtualCamera.m_Lens.OrthographicSize; 
        float endTime = 0.0f;

        while (startTime > endTime)
        {
            startTime -= Time.deltaTime + 0.1f;
            //Debug.Log($"from Decrease startTime - {startTime} : endTime - {endTime}");
            virtualCamera.m_Lens.OrthographicSize = defaultSize + startTime;

            yield return null;
        }
    }

}