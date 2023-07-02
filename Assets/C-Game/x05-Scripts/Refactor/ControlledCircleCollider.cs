using UnityEngine;
using System.Collections.Generic;

public class ControlledCircleCollider : MonoBehaviour
{
    [SerializeField] protected float m_CircleRadius = 0.45F;
    [SerializeField] protected float m_Density = 1.0F;

    protected List<Collider2D> m_Colliders = new List<Collider2D>();

    private CircleCollider2D m_CircleCollider2D;

    public delegate void OnCollisionEnter(Collision2D a_collision2D);
    public event OnCollisionEnter OnCollisionEnterEvent;

    public delegate void OnCollisionExit(Collision2D a_collision2D);
    public event OnCollisionExit OnCollisionExitEvent;
    
    private void OnEnable()
    {
        
    }

    private void OnDisable() 
    {

    }

    private void Awake()
    {
        m_CircleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        m_CircleCollider2D.radius = m_CircleRadius;
        m_CircleCollider2D.density = m_CircleRadius;
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D a_collisionInfo)
    {
        OnCollisionEnterEvent?.Invoke(a_collisionInfo);
    }

    private void OnCollisionExit2D(Collision2D a_collisionInfo) 
    {
        OnCollisionExitEvent?.Invoke(a_collisionInfo);
    }
}