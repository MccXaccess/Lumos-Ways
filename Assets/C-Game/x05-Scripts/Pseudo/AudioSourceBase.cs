using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Audio Source Base", menuName = "Game Variables/Create New Audio Source Base", order = 0)]
public class AudioSourceBase : ScriptableObject
{
    [Header("Sound Effects:")]
    [Space(15)]

    [Header("Character Sounds:")]
    [SerializeField] private AudioClip m_DeathSound;
    [SerializeField] private AudioClip m_StickedSound;
    [SerializeField] private AudioClip m_JumpedSound;
    [SerializeField] private AudioClip m_BouncedSound;

    [Header("Environment Sounds:")]
    [SerializeField] private AudioClip m_PreExplosionSound;
    [SerializeField] private AudioClip m_ExplosionSound;
    [SerializeField] private AudioClip m_ShootingBlockSound;


    [Header("Sound Tracks")]
    [Space(15)]

    [SerializeField] private AudioClip m_SoundTrack_MainMenu;

    public List<AudioClip> m_StageSoundTracks = new List<AudioClip>();

    private int m_MusicIndex;

    #region Encapsulation - Sound Effects
    public AudioClip DeathSound { get => m_DeathSound; set => m_DeathSound = value; }
    public AudioClip StickedSound { get => m_StickedSound; set => m_StickedSound = value; }
    public AudioClip JumpedSound { get => m_JumpedSound; set => m_JumpedSound = value; }
    public AudioClip BouncedSound { get => m_BouncedSound; set => m_BouncedSound = value; }

    public AudioClip PreExplosionSound { get => m_PreExplosionSound; set => m_PreExplosionSound = value; }
    public AudioClip ExplosionSound { get => m_ExplosionSound; set => m_ExplosionSound = value; }
    public AudioClip ShootingBlockSound { get => m_ShootingBlockSound; set => m_ShootingBlockSound = value; }
    #endregion

    #region Encapsulation - Sound Tracks
    public AudioClip MainMenu { get => m_SoundTrack_MainMenu; set => m_SoundTrack_MainMenu = value; }
    #endregion

    public void PlaySound(AudioSource a_AudioSource, string a_SoundName)
    {
        AudioClip audioClip = null;

        audioClip = GetClip(a_SoundName);

        if (audioClip != null)
        {
            StopSound(a_AudioSource);
            a_AudioSource.clip = audioClip;
            a_AudioSource.Play();
        }
    }

    public void StopSound(AudioSource a_AudioSource)
    {
        a_AudioSource.Stop();
    }

    public AudioClip GetClip(string a_SoundName)
    {
        AudioClip audioClip = null;

        switch (a_SoundName)
        {
            // Sound Effects
            case "DeathSound":
                audioClip = m_DeathSound;
                break;
            case "StickedSound":
                audioClip = m_StickedSound;
                break;
            case "JumpedSound":
                audioClip = m_JumpedSound;
                break;
            case "BouncedSound":
                audioClip = m_BouncedSound;
                break;
            case "PreExplosionSound":
                audioClip = m_PreExplosionSound;
                break;
            case "ExplosionSound":
                audioClip = m_ExplosionSound;
                break;
            case "ShootingBlockSound":
                audioClip = m_ShootingBlockSound;
                break;

            // Sound Tracks
            case "MainMenu":
                audioClip = m_SoundTrack_MainMenu;
                break;

            default:
                Debug.LogWarning("Sound name not found: " + a_SoundName);
                break;
        }

        return audioClip;
    }

    public void InitiateActions()
    {
        m_MusicIndex = 0;
    }
    public void NextStageMusic(AudioSource a_AudioSource)
    {
        if (m_MusicIndex >= m_StageSoundTracks.Count)
        {
            m_MusicIndex = 0;
        }

        StopSound(a_AudioSource);
        a_AudioSource.clip = m_StageSoundTracks[m_MusicIndex];
        a_AudioSource.Play();
        
        m_MusicIndex++;
    }
}