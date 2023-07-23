using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlledCircleCollider : MonoBehaviour, IUnparent
{
    [SerializeField] private string m_NextStageTag;
    [SerializeField] private string m_DeathTag;
    [SerializeField] private string m_IgnoreCollisionTag;
    [SerializeField] private string m_DoNotParentTag;

    [SerializeField] protected float m_CircleRadius = 0.45F;
    [SerializeField] protected float m_Density = 1.0F;

    [SerializeField] protected LayerMask m_LayerMask;

    private CircleCollider2D m_CircleCollider2D;
    
    private LumoController m_LumoController;

    [Space(10)]
    [SerializeField] private GameObject m_ExplosionParticle;

    private Transform m_CollisionObject;

    [SerializeField] private bool m_CooldownPassed = true;
    [SerializeField] private bool m_CollisionDetected;

    public float m_CooldownDuration;

    public AudioSourceBase m_AudioSourceBase;
    public AudioSource m_AudioSource;

    private void OnEnable() 
    {
        m_LumoController.OnJump += OnJumpHandler;
    }

    private void OnDisable() 
    {
        m_LumoController.OnJump -= OnJumpHandler;
    }
    private void Awake()
    {
        m_CircleCollider2D = GetComponent<CircleCollider2D>();
        m_LumoController = GetComponent<LumoController>();
    }

    private void Start()
    {
        //m_CircleCollider2D.radius = m_CircleRadius;
        //m_CircleCollider2D.density = m_CircleRadius;
    }

    private void Update() 
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.down, m_CircleRadius, m_LayerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.up, m_CircleRadius, m_LayerMask);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, m_CircleRadius, m_LayerMask);

        m_CollisionDetected = colliders.Length > 0;

        if (hit1.collider != null && hit2.collider != null)
        {
            WhenDead();
            return;
        }

        if (m_CollisionDetected && colliders[0].gameObject.CompareTag(m_DeathTag))
        {
            WhenDead();
            return;
        }

        if (m_CollisionDetected && colliders[0].gameObject.CompareTag(m_NextStageTag))
        {
            colliders[0].gameObject.SetActive(false);
            GameManager.Instance.InitWin();
            return;
        }

        if (m_CollisionDetected && colliders[0].gameObject.CompareTag(m_DoNotParentTag))
        {
            FreezePlayer(true);
            return;
        }

        if (m_CollisionDetected && colliders[0].gameObject.CompareTag(m_IgnoreCollisionTag))
        {
            return;
        }

        if (m_CollisionDetected && m_CooldownPassed)
        {
            //m_AudioSourceBase.PlaySound(m_AudioSource, "StickedSound");
            FreezePlayer(true);
            ParentPlayer(colliders[0].gameObject.transform);
            return;
        }

        FreezePlayer(false);
        ParentPlayer(null);
    }

    // private void OnCollisionEnter2D(Collision2D other) 
    // {
    //     if (other.gameObject.CompareTag("Death"))
    //     {
    //         WhenDead();
    //     }
    // }

    public void Unparent()
    {
        ParentPlayer(null);
    }

    private void OnJumpHandler()
    {
        OnJumpAction();
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(m_CooldownDuration);
        m_CooldownPassed = true;
    }

    private void WhenDead()
    {
        //m_AudioSourceBase.PlaySound(m_AudioSource, "DeathSound");
        ParentPlayer(null);
        FreezePlayer(false);

        if (m_ExplosionParticle != null)
            Instantiate(m_ExplosionParticle, transform.position, Quaternion.identity);

        GameManager.Instance.Dead();
    }
    public void OnJumpAction()
    {
        //m_AudioSourceBase.PlaySound(m_AudioSource, "JumpedSound");
        m_CooldownPassed = false;
        FreezePlayer(false);
        ParentPlayer(null);
        StartCoroutine(Cooldown());
    }

    public void FreezePlayer(bool a_State)
    {
        m_LumoController.m_RB2D.bodyType = a_State ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
    }

    private void ParentPlayer(Transform a_Transform)
    {
        transform.SetParent(a_Transform);
    }
}