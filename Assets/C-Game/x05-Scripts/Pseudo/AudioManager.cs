using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer mixer;

    public AudioSource audioSource;
    public List<AudioClip> playerClips = new List<AudioClip>();

    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        LoadVolume();    
    }

    public void DeathSFX()
    {
        AudioClip clip = playerClips[Random.Range(0,playerClips.Count)];

        audioSource.PlayOneShot(clip);
    }

    void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY);

        //Debug.Log(musicVolume);

        mixer.SetFloat("MusicVolumeParam"/*VoumeSettings.MIXER_MUSIC*/, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat("SFXVolumeParam"/*VoumeSettings.MIXER_SFX*/, Mathf.Log10(sfxVolume) * 20);
    }
}