using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTransition;

public class PauseManager : MonoBehaviour
{
    public GameObject[] m_AffectedObjects;
    public GameObject[] m_AffectedObjectsReversed;
    private bool m_CurrentState;
    public AudioSource m_AudioPlayer;

    public void PauseToggle()
    {
        if (TransitionManager.Instance().runningTransition)
        {
            // Don't pause or resume the game if a transition is running
            return;
        }

        m_CurrentState = !m_CurrentState;

        if (m_CurrentState)
        {
            GameManager.Instance.m_Interactable = false;
            m_AudioPlayer.volume = 0.5F;
        }
        else 
        {
            GameManager.Instance.m_Interactable = true;
            m_AudioPlayer.volume = 1;
        }

        foreach (GameObject obj in m_AffectedObjects)
        {
            obj.SetActive(m_CurrentState);
        }

        foreach (GameObject obj in m_AffectedObjectsReversed)
        {
            obj.SetActive(!m_CurrentState);
        }

        Time.timeScale = m_CurrentState ? 0 : 1;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && GameManager.Instance.m_CanPause)
        {
            PauseToggle();
        }
    }
}