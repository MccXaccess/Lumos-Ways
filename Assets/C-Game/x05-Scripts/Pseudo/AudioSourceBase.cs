using UnityEngine;

[CreateAssetMenu(fileName = "Generation", menuName = "Base Character Controller/Create New Configuration", order = 0)]
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
    [SerializeField] private AudioClip m_SoundTrack_Stage1;
    [SerializeField] private AudioClip m_SoundTrack_Stage2;
    [SerializeField] private AudioClip m_SoundTrack_Stage3;
    [SerializeField] private AudioClip m_SoundTrack_Stage4;
    [SerializeField] private AudioClip m_SoundTrack_Stage5;
    [SerializeField] private AudioClip m_SoundTrack_Stage6;
    [SerializeField] private AudioClip m_SoundTrack_Stage7;
    [SerializeField] private AudioClip m_SoundTrack_Stage8;
    [SerializeField] private AudioClip m_SoundTrack_Stage9;
    [SerializeField] private AudioClip m_SoundTrack_Stage10;

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
    public AudioClip Stage1 { get => m_SoundTrack_Stage1; set => m_SoundTrack_Stage1 = value; }
    public AudioClip Stage2 { get => m_SoundTrack_Stage2; set => m_SoundTrack_Stage2 = value; }
    public AudioClip Stage3 { get => m_SoundTrack_Stage3; set => m_SoundTrack_Stage3 = value; }
    public AudioClip Stage4 { get => m_SoundTrack_Stage4; set => m_SoundTrack_Stage4 = value; }
    public AudioClip Stage5 { get => m_SoundTrack_Stage5; set => m_SoundTrack_Stage5 = value; }
    public AudioClip Stage6 { get => m_SoundTrack_Stage6; set => m_SoundTrack_Stage6 = value; }
    public AudioClip Stage7 { get => m_SoundTrack_Stage7; set => m_SoundTrack_Stage7 = value; }
    public AudioClip Stage8 { get => m_SoundTrack_Stage8; set => m_SoundTrack_Stage8 = value; }
    public AudioClip Stage9 { get => m_SoundTrack_Stage9; set => m_SoundTrack_Stage9 = value; }
    public AudioClip Stage10 { get => m_SoundTrack_Stage10; set => m_SoundTrack_Stage10 = value; }
    #endregion
}