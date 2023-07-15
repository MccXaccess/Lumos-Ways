using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManipulatorOfWindows : MonoBehaviour
{
    [SerializeField] private ManipulateWindow m_WindowManipulate;

    void Start()
    {
        CloseClicked();
        OpenClicked();
    }

    private void CloseClicked()
    {
        m_WindowManipulate.gameObject.SetActive(true);
        m_WindowManipulate.m_CloseWindowButton.onClick.AddListener(WhenButtonClicked);
    }

    private void OpenClicked()
    {
        m_WindowManipulate.gameObject.SetActive(true);
        m_WindowManipulate.m_OpenWindowButton.onClick.AddListener(WhenButtonClicked);
    }

    private void WhenButtonClicked()
    {
        m_WindowManipulate.gameObject.SetActive(false);
    }
}