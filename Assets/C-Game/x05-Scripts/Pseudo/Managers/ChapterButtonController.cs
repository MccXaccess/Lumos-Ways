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

    private void Start()
    {
        Button button = GetComponent<Button>();
        Image image = GetComponent<Image>();

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

    public void ButtonLoadScene()
    {
        m_LoadTransitionScene.LoadScene(m_ChapterIndex);
    }
}