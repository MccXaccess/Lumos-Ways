using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelButton : MonoBehaviour
{
    public string m_PanelID;

    public PanelShowBehaviour m_Behaviour;

    private PanelManager m_PanelManager;

    private void Start()
    {
        m_PanelManager = PanelManager.Instance;
    }

    public void DoShowPanel()
    {
        m_PanelManager.ShowPanel(m_PanelID, m_Behaviour);
    }
}