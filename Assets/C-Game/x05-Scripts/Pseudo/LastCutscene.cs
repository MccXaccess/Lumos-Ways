using System.Collections;
using UnityEngine;
using TMPro;
using EasyTransition;

public class LastCutscene : MonoBehaviour
{
    public TextMeshProUGUI m_DialogueText;
    public DemoLoadScene m_FadeOutTransition;
    public GameObject m_TitlesScreen;
    public AudioSource m_AudioSource;
    public bool m_IgnoreTime;
    public float m_TextScrollSpeed = 50f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (m_IgnoreTime)
            {
                m_FadeOutTransition?.LoadScene(0);
                m_TitlesScreen.SetActive(true);
                return;
            }

            m_DialogueText.color = new Color(0, 0, 0, 1);
            GameManager.Instance.m_CanPause = false;
            GameManager.Instance.m_Interactable = false;
            StartCoroutine(ShowEndTitles());
        }
    }

    IEnumerator ShowEndTitles()
    {
        // Wait for 45 seconds before fading out the scene
        yield return new WaitForSeconds(45f);

        // Fade out the scene transition
        m_FadeOutTransition?.LoadScene("Main Menu");

        // Wait for 7.5 seconds after the scene fades out
        yield return new WaitForSeconds(7.5f);

        // Activate the end titles screen
        m_TitlesScreen.SetActive(true);

        // Calculate the total time it will take for the text to scroll off the screen
        float scrollTime = m_DialogueText.preferredWidth / m_TextScrollSpeed;

        // Move the text off the screen smoothly
        float elapsedTime = 0f;
        Vector3 initialPos = m_DialogueText.rectTransform.position;
        Vector3 targetPos = m_DialogueText.rectTransform.position + Vector3.up * Screen.height;

        while (elapsedTime < scrollTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / scrollTime);
            m_DialogueText.rectTransform.position = Vector3.Lerp(initialPos, targetPos, t);
            yield return null;
        }

        // Wait for 22.5 seconds after the text scrolls off the screen
        yield return new WaitForSeconds(10);

        // Fade out the audio
        while (m_AudioSource.volume > 0.05f)
        {
            m_AudioSource.volume -= Time.deltaTime * 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
    }
}