using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using EasyTransition;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform m_StartPoint;

    public TextMeshProUGUI m_TMProCurrentStageTitle;

    public GameObject m_DeadUI;

    private bool m_IsAlive;
    
    public GameObject player;
    
    public PlayerState m_CurrentState;
    
    public DemoLoadScene demoLoadScene;

    public bool m_Interactable;

    public string m_StageName;

    private bool m_WinInitiated;

    public enum PlayerState
    {
        DEAD,
        ALIVE,
        RESPAWN,
    }

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        m_CurrentState = PlayerState.ALIVE;
        player.transform.position = m_StartPoint.position;
        InitiateStages();
    }

    private IEnumerator AlphaAnimationCoroutine()
    {
        // Increase alpha from 0 to 1
        float duration = 1f;
        float elapsedTime = 0f;
        Color startColor = m_TMProCurrentStageTitle.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            Color newColor = Color.Lerp(startColor, endColor, t);
            m_TMProCurrentStageTitle.color = newColor;
            yield return null;
        }

        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Decrease alpha from 1 to 0
        duration = 1f;
        elapsedTime = 0f;
        startColor = m_TMProCurrentStageTitle.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            Color newColor = Color.Lerp(startColor, endColor, t);
            m_TMProCurrentStageTitle.color = newColor;
            yield return null;
        }
    }

    public void Dead()
    {
        m_CurrentState = PlayerState.DEAD;

        player.SetActive(false);
        m_DeadUI.SetActive(true);
    }

    public void Respawn()
    {
        // is it even neccessary?
        //GameSceneManager.Instance.ReloadScene();

        m_CurrentState = PlayerState.ALIVE;

        player.SetActive(true);
        m_DeadUI.SetActive(false);

        player.transform.position = m_StartPoint.position;
    }

    private void InitiateStages()
    {
        m_TMProCurrentStageTitle.text = m_StageName;
        
        StartCoroutine(AlphaAnimationCoroutine());
    }

    private void NextStage()
    {
        // if (!m_WinInitiated)
        // {
        //     demoLoadScene?.LoadScene(GameSceneManager.Instance.GetRelativeSceneIndex());
        //     m_WinInitiated = true;
        // }
        demoLoadScene?.LoadScene(GameSceneManager.Instance.GetRelativeSceneIndex());
    }

    public void ReloadGame()
    {
        m_CurrentState = PlayerState.ALIVE;
        player.SetActive(true);
        m_DeadUI.SetActive(false);
    }

    public void InitWin()
    {
        NextStage();
    }

    // private void SetActive(List <GameObject> a_Objects, bool a_State = false)
    // {
    //     foreach (GameObject obj in a_Objects)
    //     {
    //         obj.SetActive(a_State);
    //     }
    // }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R) && m_CurrentState == PlayerState.DEAD)
        {
            m_CurrentState = PlayerState.RESPAWN;
        }

        switch (m_CurrentState)
        {
            case PlayerState.ALIVE:
                m_IsAlive = true;
                return;

            case PlayerState.RESPAWN:
                Respawn();
                break;

            case PlayerState.DEAD:
                m_IsAlive = false;
                break;
        }
    }
}