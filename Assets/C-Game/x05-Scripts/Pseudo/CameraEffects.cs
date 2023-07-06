using UnityEngine;
using Cinemachine;

public class CameraEffects : CinemachineExtension
{
    public float moveDistance = 2f;
    public float moveSpeed = 1f;
    public float scaleModifier = 0.2f;

    private Vector3 originalPosition;
    private float timer;

    protected override void Awake()
    {
        base.Awake();
        originalPosition = transform.position;
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            // Calculate the horizontal movement using a sine wave
            float xOffset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
            float yOffset = Mathf.Cos(Time.time * moveSpeed) * moveDistance / 2;

            // Apply the horizontal movement and vertical scaling to the camera state
            state.PositionCorrection += new Vector3(xOffset, yOffset, 0f);
            state.Lens.OrthographicSize = state.Lens.OrthographicSize + Mathf.Sin(Time.time * moveSpeed) * scaleModifier;
        }
    }
}