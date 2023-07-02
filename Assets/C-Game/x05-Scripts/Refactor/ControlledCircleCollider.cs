using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlledCircleCollider : MonoBehaviour
{
    [SerializeField] private string m_NextStageTag;
    [SerializeField] private string m_DeathTag;
    [SerializeField] private string m_IgnoreSurfaceTag;

    [SerializeField] protected float m_CircleRadius = 0.45F;
    [SerializeField] protected float m_Density = 1.0F;

    private CircleCollider2D m_CircleCollider2D;
    private LumoController m_LumoController;

    private bool m_CooldownPassed = true;

    public float m_TimeJumpAmount;

    private void Awake()
    {
        m_CircleCollider2D = GetComponent<CircleCollider2D>();
        m_LumoController = GetComponent<LumoController>();
        m_LumoController.OnJump += OnJumpHandler;
    }

    private void Start()
    {
        m_CircleCollider2D.radius = m_CircleRadius;
        m_CircleCollider2D.density = m_CircleRadius;
    }

    private void OnCollisionEnter2D(Collision2D a_collisionInfo)
    {
        if (m_CooldownPassed)
        {
            m_CooldownPassed = false;
            FreezePlayer(true);
        }
    }

    public void OnJumpAction()
    {
        FreezePlayer(false);
        StartCoroutine(Cooldown());
    }

    public void FreezePlayer(bool a_State)
    {
        m_LumoController.m_RB2D.bodyType = a_State ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(m_TimeJumpAmount);
        m_CooldownPassed = true;
    }

    private void OnJumpHandler()
    {
        OnJumpAction();
    }
}