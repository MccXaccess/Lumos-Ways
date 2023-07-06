using UnityEngine;
using Cinemachine;

public class CinemachineViewportAdjuster : MonoBehaviour
{
    public float targetViewportHeight = 0.5f; // Desired height of the visible area as a percentage of the screen height
    public float transitionSpeed = 1f; // Speed at which the orthographic size adjusts

    private CinemachineVirtualCamera virtualCamera;
    private float targetOrthographicSize;
    private float originalOrthographicSize;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        AdjustCameraViewport();
        originalOrthographicSize = virtualCamera.m_Lens.OrthographicSize;
    }

    private void AdjustCameraViewport()
    {
        float targetHeight = targetViewportHeight * Screen.height;
        float targetWidth = targetHeight * Screen.width / Screen.height;

        targetOrthographicSize = targetWidth / (2f * virtualCamera.m_Lens.Aspect);
    }

    private void Update()
    {
        float currentOrthographicSize = virtualCamera.m_Lens.OrthographicSize;
        float newOrthographicSize = Mathf.Lerp(currentOrthographicSize, targetOrthographicSize, transitionSpeed * Time.deltaTime);
        virtualCamera.m_Lens.OrthographicSize = newOrthographicSize;
    }
}
