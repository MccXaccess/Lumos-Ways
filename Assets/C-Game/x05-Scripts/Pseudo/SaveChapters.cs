using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveChapters : MonoBehaviour
{
    public static SaveChapters Instance;

    public int m_UnlockedLevels;
    private const string UNLOCKED_LEVELS_KEY = "UnlockedLevels";

    // Event for level unlock
    // but in this sutuation i think it's appropriate to use
    // checklockedstatus
    public delegate void OnLevelModify();
    public event OnLevelModify onLevelModify;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);

        m_UnlockedLevels = GetUnlockedLevelsValue();

        if (m_UnlockedLevels > 10)
        {
            PlayerPrefs.SetInt(UNLOCKED_LEVELS_KEY, 10); 
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt(UNLOCKED_LEVELS_KEY, 1);
        UpdateUnlockedLevelsValue();
    }

    public void IncrementValue()
    {
        m_UnlockedLevels = GetUnlockedLevelsValue();
        m_UnlockedLevels++;
        PlayerPrefs.SetInt(UNLOCKED_LEVELS_KEY, m_UnlockedLevels);
    }

    public int GetUnlockedLevelsValue()
    {
        return PlayerPrefs.GetInt(UNLOCKED_LEVELS_KEY, 1);
    }

    private void UpdateUnlockedLevelsValue()
    {
        m_UnlockedLevels = GetUnlockedLevelsValue();
        PlayerPrefs.SetInt(UNLOCKED_LEVELS_KEY, m_UnlockedLevels);
        onLevelModify?.Invoke();
    }

    public bool CheckLockedStatus(int a_levelIndex)
    {
        if (a_levelIndex <= m_UnlockedLevels) { return true; }
        else { return false; }
    }
}