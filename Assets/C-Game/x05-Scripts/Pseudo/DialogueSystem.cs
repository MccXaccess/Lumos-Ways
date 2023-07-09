using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    // make an event to trigger the dialogue
    // make a sentence arrays in another class? or Scripotable Object?
    // iterate trough every senetece and change all of the tmpro texts to current dialogue text
    // make a base string and every tmpro inhertis from it? like the text?
    // should thihs connect to gameManager and from thhere being triggeered?

    public TextMeshProUGUI[] m_DialogueText;

    // scriptable objects with sentences?
    public string[] m_DialogueSentence;

    private int m_CurrentDialogueIndex;

    public GameObject m_DialogueWindow;

    private void Awake()
    {
        m_CurrentDialogueIndex = 0;
    }

    private void Update() 
    {
        if (Input.GetMouseButtonUp(0))    
        {
            NextDialogueSentence();
        }
    }

    public void NextDialogueSentence()
    {
        if (m_CurrentDialogueIndex >= m_DialogueSentence.Length)
        {
            FinishDialogueWindow();
        }

        //IterateTroughTMPs(m_DialogueSentence[m_CurrentDialogueIndex]);
        m_CurrentDialogueIndex++;
    }

    private void IterateTroughTMPs(string a_TextToAssign)
    {
        foreach (var dialogue in m_DialogueText)
        {
            //m_DialogueSentence[m_CurrentDialogueIndex].text = a_TextToAssign;
        }
    }

    private void FinishDialogueWindow()
    {
        m_CurrentDialogueIndex = 0;
        m_DialogueWindow.SetActive(false);
    }

    public void StartDialogueWindow()
    {
        m_CurrentDialogueIndex = 0;
        m_DialogueWindow.SetActive(true);
    }
}