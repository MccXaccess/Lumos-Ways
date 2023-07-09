using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public string[] dialogueSentences;

    private bool dialogueStarted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueStarted)
        {
            dialogueStarted = true;
            foreach (string sentence in dialogueSentences)
            {
                dialogueManager.AddSentence(sentence);
            }
        }
    }
}