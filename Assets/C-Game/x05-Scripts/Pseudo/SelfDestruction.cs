using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    [SerializeField] private float m_TimeBeforeDestruction = 1F;
    private void Start()
    {
        Destroy(gameObject, m_TimeBeforeDestruction);
    }
}