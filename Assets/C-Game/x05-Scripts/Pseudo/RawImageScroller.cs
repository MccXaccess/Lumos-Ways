using UnityEngine;
using UnityEngine.UI;

public class RawImageScroller : MonoBehaviour
{
    [SerializeField] private RawImage m_Image;
    [SerializeField] private float m_X, m_Y;

    private void Update()
    {
        m_Image.uvRect = new Rect(m_Image.uvRect.position + new Vector2(m_X, m_Y) * Time.deltaTime, m_Image.uvRect.size);
    }
}