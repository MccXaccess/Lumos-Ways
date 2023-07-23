using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterButtonController : MonoBehaviour
{
    public int m_ChapterIndex;
    public DemoLoadScene m_LoadTransitionScene;
    public GameObject m_LockedLevelObject;

    private Button button;
    private Image image;

    private void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        CheckAcessibility();
    }

    public void ButtonLoadScene()
    {
        m_LoadTransitionScene.LoadScene(m_ChapterIndex);
    }

    private void CheckAcessibility()
    {
        if (SaveChapters.Instance.CheckLockedStatus(m_ChapterIndex))
        {
            button.interactable = true;
            m_LockedLevelObject.SetActive(false);
            //image.color = new Color32(color.R, color.G, color.B, 255);
            return;
        }

        //image.color = new Color(color.r, color.g, color.b, 200);
        m_LockedLevelObject.SetActive(true);
        button.interactable = false;
    }

    private void OnEnable()
    {
        // Subscribe to the event when the object is enabled
        SaveChapters.Instance.onLevelModify += CheckAcessibility;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event when the object is disabled
        SaveChapters.Instance.onLevelModify -= CheckAcessibility;
    }
}