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
    //public UnityEvent<int> OnLevelUnlock;

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

        if (m_UnlockedLevels > 9)
        {
            PlayerPrefs.SetInt(UNLOCKED_LEVELS_KEY, 9); 
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt(UNLOCKED_LEVELS_KEY, 1);
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

    public void ResetValue()
    {
        PlayerPrefs.DeleteKey(UNLOCKED_LEVELS_KEY);
    }

    public bool CheckLockedStatus(int a_levelIndex)
    {
        if (a_levelIndex <= m_UnlockedLevels + 1) { return true; }
        else { return false; }
    }
}