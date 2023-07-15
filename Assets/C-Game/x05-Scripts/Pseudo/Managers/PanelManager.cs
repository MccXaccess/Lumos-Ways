using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PanelManager : Singleton<PanelManager>
{
    //Pool of panels
    private ObjectPool m_ObjectPool;

    //private Queue<PanelInstanceModel> m_Queue = new Queue<PanelInstanceModel>();
    private List<PanelInstanceModel> m_PanelInstancesList = new List<PanelInstanceModel>();

    private void Start()
    {
        m_ObjectPool = ObjectPool.Instance;
    }

    public void ShowPanel(string a_PanelID, PanelShowBehaviour a_Behaviour = PanelShowBehaviour.KEEP_PREVIOUS)
    {
        GameObject panelInstance = m_ObjectPool.GetObjectFromPool(a_PanelID);//m_Panels.FirstOrDefault(panel => panel.m_PanelID == a_PanelID);

        if (panelInstance != null)
        {
            //var newInstancePanel = Instantiate(panelModel.m_PanelPrefab, transform);

            if (a_Behaviour == PanelShowBehaviour.HIDE_PREVIOUS && AnyPanelShowing())
            {
                var lastPanel = GetLastPanel();

                if (lastPanel != null)
                {
                    lastPanel.m_PanelInstance.SetActive(false);
                }
            }

            //m_Queue.Enqueue(new PanelInstanceModel
            m_PanelInstancesList.Add(new PanelInstanceModel
            {
                m_PanelID = a_PanelID,
                m_PanelInstance = panelInstance//newInstancePanel
            });
        }
        else
        {
            Debug.LogWarning($"Trying to use m_PanelID = {a_PanelID}, but was not found in m_Panels!");
        }
    }

    public void HideLastPanel()
    {
        if (AnyPanelShowing())
        {
            //var lastPanel = m_Queue.Dequeue();
            var lastPanel = GetLastPanel();

            m_PanelInstancesList.Remove(lastPanel);

            m_ObjectPool.PoolObject(lastPanel.m_PanelInstance);
            //Destroy(lastPanel.m_PanelInstance);

            if (AnyPanelShowing())
            {
                lastPanel = GetLastPanel();

                if(lastPanel != null && !lastPanel.m_PanelInstance.activeInHierarchy)
                {
                    lastPanel.m_PanelInstance.SetActive(true);
                }
            }
        }
    }

    PanelInstanceModel GetLastPanel()
    {
        return m_PanelInstancesList[m_PanelInstancesList.Count - 1];
    }

    public bool AnyPanelShowing()
    {
        return GetAmountPanelsInQueue() > 0;
    }

    public int GetAmountPanelsInQueue()
    {
        //return m_Queue.Count;
        return m_PanelInstancesList.Count;
    }
}