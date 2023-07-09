using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VoumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer m_Mixer;
    [SerializeField] Slider m_MusicSlider;
    [SerializeField] Slider m_SFXSlider;

    public const string MIXER_MUSIC = "MusicVolumeParam";
    public const string MIXER_SFX = "SFXVolumeParam";

    void Awake()
    {
        m_MusicSlider.onValueChanged.AddListener(SetMusicVolume);
        m_SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void Start()
    {
        m_MusicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        m_SFXSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, m_MusicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, m_SFXSlider.value);
    }
    
    void SetMusicVolume(float value)
    {
        m_Mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    void SetSFXVolume(float value)
    {
        m_Mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }
}