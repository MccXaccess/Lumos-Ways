using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EasyTransition;

public class LastCutscene : MonoBehaviour
{
    public TextMeshProUGUI m_DialogueText;
    public DemoLoadScene m_FadeOutTransition;
    public GameObject m_TitlesScreen;
    public AudioSource m_AudioSource;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_DialogueText.color = new Color(0,0,0,1);
            GameManager.Instance.m_CanPause = false;
            GameManager.Instance.m_Interactable = false;
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(40);
        m_FadeOutTransition?.LoadScene(0);
        yield return new WaitForSeconds(1);
        m_TitlesScreen.SetActive(true);
        yield return new WaitForSeconds(21.5f);

        while (m_AudioSource.volume > 0.1f)
        {
            m_AudioSource.volume -= Time.deltaTime * 0.2f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}