using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    private Queue<string> sentenceQueue;
    private bool isDisplayingDialogue;
    private float dialogueCooldown = 2f;
    private float typingSpeed = 0.05f;

    private Coroutine typewriterCoroutine;

    private void Start()
    {
        sentenceQueue = new Queue<string>();
    }

    private void Update()
    {
        if (!isDisplayingDialogue && sentenceQueue.Count > 0)
        {
            string sentence = sentenceQueue.Dequeue();
            typewriterCoroutine = StartCoroutine(DisplaySentence(sentence));
        }
    }

    public void AddSentence(string sentence)
    {
        sentenceQueue.Enqueue(sentence);
    }

    private IEnumerator DisplaySentence(string sentence)
    {
        isDisplayingDialogue = true;
        dialogueText.text = string.Empty;

        for (int i = 0; i < sentence.Length; i++)
        {
            if (sentence[i] == '*')
            {
                sentence = sentence.Remove(i, 1);
                i--; // Decrement i to adjust for the removed character
                yield return new WaitForSeconds(0.4f);
            }
            else
            {
                dialogueText.text += sentence[i];
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        yield return new WaitForSeconds(1f);

        isDisplayingDialogue = false;
        dialogueText.text = string.Empty;
    }
}