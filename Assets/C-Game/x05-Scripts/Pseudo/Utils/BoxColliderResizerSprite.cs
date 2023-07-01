using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// I DID ALL OF THAT TO PREVENT BUGGY RESIZE OF THE PLAYER RELATIVE TO THE ATTACHED PARENT SO IT WILL BE SYMMETRIC
/// </summary> <summary>
/// & i didn't come up to any good idea rather than this?
/// </summary>
public class BoxColliderResizerSprite : MonoBehaviour
{
    [SerializeField] private Transform sprite;
    [SerializeField] private BoxCollider2D boxCollider2D;

    private Vector2 savedSize;

    private void Start()
    {
        savedSize = sprite.localScale;
        ApplySizeToBoxCollider();
    }

    private void OnValidate()
    {
        if (boxCollider2D != null && sprite != null)
        {
            savedSize = sprite.localScale;
            ApplySizeToBoxCollider();
        }
    }

    private void ApplySizeToBoxCollider()
    {
        boxCollider2D.size = savedSize;
    }
}