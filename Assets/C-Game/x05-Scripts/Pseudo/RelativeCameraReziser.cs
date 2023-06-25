using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeCameraReziser : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    public float additionalModifier;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        ResizeSprite();
    }

    private void ResizeSprite()
    {
        if (spriteRenderer == null || Camera.main == null)
            return;

        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        float spriteHeight = spriteRenderer.sprite.bounds.size.y;
        float spriteWidth = spriteRenderer.sprite.bounds.size.x;

        Vector3 scale = transform.localScale;
        scale.x = cameraWidth / spriteWidth + additionalModifier;
        scale.y = cameraHeight / spriteHeight + additionalModifier;
        transform.localScale = scale;
    }

    private void Update()
    {
        ResizeSprite();
    }
}