using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int levelIndex;
    public SaveChapters saveChapters;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        // Subscribe to the event
        //saveChapters.OnLevelUnlock.AddListener(OnLevelUnlocked);

        if (saveChapters.CheckLockedStatus(levelIndex))
        {
            button.interactable = true; 
            return;
        }

        button.interactable = false;
    }

    private void OnDisable() 
    {
        //saveChapters.OnLevelUnlock.RemoveListener(OnLevelUnlocked);    
    }
}