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
        float endTime = maxOrthoSize - virtualCamera.m_Lens.OrthographicSize;

        while ( startTime < endTime )
        {
            startTime += Time.deltaTime + 0.01f;

            virtualCamera.m_Lens.OrthographicSize = defaultSize + startTime;
            
            yield return null;
        }
    }

    private IEnumerator DecreaseOrtho()
    {
        float startTime = virtualCamera.m_Lens.OrthographicSize;
        float endTime = defaultSize;

        while ( startTime > endTime )
        {
            startTime -= Time.deltaTime + 0.1f;

            virtualCamera.m_Lens.OrthographicSize = startTime;
            
            yield return null;
        }
    }
}