using UnityEngine;

public class LumoController : MonoBehaviour
{
    [Header("Lumo Controller Values:")]
    [SerializeField] private float m_MaximumShootPower = 20.0F;
    [SerializeField] private float m_GravityModifier = 1.0F;
    
    [Space(10)]
    [SerializeField] private bool m_ForwardDragging = false;
    [SerializeField] private bool m_ShowLineOnScreen = false;
    
    [Space(10)]
    [SerializeField] private LineRenderer m_DragPlayerLine;
    [SerializeField] private LineRenderer m_DragMouseLine;

    [Space(10)]
    [SerializeField] private Transform m_Direction;

    private Vector2 m_StartPosition;
    private Vector2 m_TargetPosition;
    
    private Vector2 m_StartMousePositionScreen;
    private Vector2 m_StartMousePosition;
    private Vector2 m_CurrentMousePosition;

    private bool m_CanShoot = false;
    private bool m_IsDragging = false;

    private float m_CurrentShootPower = 0.0F;

    [HideInInspector] public Rigidbody2D m_RB2D;

    public delegate void OnJumpEvent();
    public event OnJumpEvent OnJump;

    private void Awake()
    {
        m_RB2D = GetComponent<Rigidbody2D>();   
    }

    private void Start()
    {
        m_RB2D.gravityScale = m_GravityModifier;
    }

    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {
        if (m_ShowLineOnScreen)
            DrawMouseLine();
    }

    private void Update()
    {
        if (!GameManager.Instance.m_Interactable)
        {
            m_IsDragging = false;
            return;
        }

        if (Input.GetMouseButtonDown(0) && m_RB2D.velocity.y == 0 && m_CanShoot)
        {
            MouseClick();
        }

        if (Input.GetMouseButton(0) && m_RB2D.velocity.y == 0 && m_CanShoot)
        {
            MouseDrag();
        }

        if (Input.GetMouseButtonUp(0) && m_RB2D.velocity.y == 0 && m_CanShoot)
        {
            MouseRelease();
        }

        if (m_RB2D.velocity.magnitude == 0)
        {
            m_CanShoot = true;
        }

        if (!m_IsDragging)
        {
            m_DragMouseLine.enabled = false;
            m_DragPlayerLine.enabled = false;
        }
    }

    private void MouseClick()
    {
        m_IsDragging = true;

        m_StartMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        m_StartMousePositionScreen = Input.mousePosition;

        Vector2 direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        transform.right = direction * 1;
    }

    private void MouseDrag()
    {
        LookAtShootDirection();
        CalculateShootPower();
        DrawPlayerLine();

        m_StartMousePosition = Camera.main.ScreenToWorldPoint(m_StartMousePositionScreen);
        
        m_CurrentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float distance = Vector2.Distance(m_CurrentMousePosition, m_StartMousePosition);

        if (distance > 1)
        {
            m_DragPlayerLine.enabled = true;

            if (m_ShowLineOnScreen)
                m_DragMouseLine.enabled = true;
        }
    }

    private void MouseRelease()
    {
        m_IsDragging = false;

        if (OnJump != null)
        {
            OnJump();
        }

        if (m_CanShoot)
        {
            Shoot();
            m_DragMouseLine.enabled = false;
            m_DragPlayerLine.enabled = false;
        }
    }

    private void LookAtShootDirection()
    {
        Vector3 direction = m_StartMousePosition - m_CurrentMousePosition;

        transform.right = m_ForwardDragging ? transform.right = direction * -1 : transform.right = direction;
    }

    private void CalculateShootPower()
    {
        float distance = Vector2.Distance(m_StartMousePosition, m_CurrentMousePosition);
        distance *= 4;

        if (distance < m_MaximumShootPower)
        {
            m_Direction.localPosition = new Vector2(distance / 6, 0);
            m_CurrentShootPower = distance;
        }
        else
        {
            m_Direction.localPosition = new Vector2(m_MaximumShootPower / 6, 0);
            m_CurrentShootPower = m_MaximumShootPower;
        }
    }

    private void Shoot()
    {
        m_CanShoot = false;
        //whenJump.Play();
        //stickToSurface.WhenShooted();
        m_RB2D.velocity = transform.right * m_CurrentShootPower;
    }

    private void DrawMouseLine()
    {
        m_DragMouseLine.positionCount = 1;
        m_DragMouseLine.SetPosition(0, m_StartMousePosition);

        Vector2 direction = m_StartMousePosition - m_CurrentMousePosition;

        // NOTE: DON'T FORGET TO CHANGE 5 VALUE TO SHOOT POWER VALUE?
        float distance = Mathf.Clamp(Vector2.Distance(m_StartMousePosition, m_CurrentMousePosition), 0, 5);
        
        Vector2 rotatedDirection = new Vector2(-direction.x, -direction.y);

        m_CurrentMousePosition = m_StartMousePosition + (rotatedDirection.normalized * distance);

        m_DragMouseLine.positionCount = 2;
        m_DragMouseLine.SetPosition(1, m_CurrentMousePosition);
    }

    private void DrawPlayerLine()
    {
        m_StartPosition = transform.transform.position;

        m_DragPlayerLine.positionCount = 1;
        m_DragPlayerLine.SetPosition(0, m_StartPosition);

        m_TargetPosition = m_Direction.position;

        m_DragPlayerLine.positionCount = 2;
        m_DragPlayerLine.SetPosition(1, m_TargetPosition);
    }
}