using UnityEngine;

public class CollisionProcessor : MonoBehaviour
{
    [SerializeField] private string m_NextStageTag;
    [SerializeField] private string m_DeathTag;
    [SerializeField] private string m_IgnoreSurfaceTag;

    private ControlledCircleCollider m_ControlledCircleCollider;
    private LumoController m_LumoController;

    private bool m_StickedToSurface;
    // Stick and turn physics off and set it as a parent


    // another one for unsticking

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
        m_LumoController = GetComponent<LumoController>();
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnJumpHandler()
    {
        FreezePlayer(false);
    }

    public void FreezePlayer(bool state)
    {    
        m_LumoController.m_RB2D.bodyType = state ? m_LumoController.m_RB2D.bodyType  = RigidbodyType2D.Static : m_LumoController.m_RB2D.bodyType = RigidbodyType2D.Dynamic;
    }
}