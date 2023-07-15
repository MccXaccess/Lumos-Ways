using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePanelButton : MonoBehaviour
{
    private PanelManager m_PanelManager;

    private void Start()
    {
        m_PanelManager = PanelManager.Instance;
    }

    public void DoHidePanel()
    {
        m_PanelManager.HideLastPanel();
    }
}