using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{
    private BaseCharacterStateAbstract m_CurrentState;

    [SerializeField] private StickedToSurfaceState m_StickedToSurfaceState;
    //[SerializeField] private MidAirState m_MidAirState;
    //[SerializeField] private DeathState m_DeathState;

    private void Start()
    {
        // change it to midairstate?
        m_CurrentState = m_StickedToSurfaceState;

        m_CurrentState.EnterState(this);
    }

    private void FixedUpdate()
    {
        m_CurrentState.FixedUpdateState(this);
    }

    private void Update()
    {
        m_CurrentState.UpdateState(this);
    }

    public void SwitchState(BaseCharacterStateAbstract a_State)
    {
        a_State.ExitState(this);

        m_CurrentState = a_State;
        
        a_State.EnterState(this);
    }
}