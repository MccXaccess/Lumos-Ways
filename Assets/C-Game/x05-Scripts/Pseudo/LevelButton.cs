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

        CheckAcessibility();
    }

    private void CheckAcessibility()
    {
        

        // Subscribe to the event
        //saveChapters.OnLevelUnlock.AddListener(OnLevelUnlocked);

        if (SaveChapters.Instance.CheckLockedStatus(levelIndex))
        {
            button.interactable = true; 
            return;
        }

        button.interactable = false;
    }

    private void OnEnable()
    {
        // Subscribe to the event when the object is enabled
        SaveChapters.Instance.onLevelModify += CheckAcessibility;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event when the object is disabled
        SaveChapters.Instance.onLevelModify -= CheckAcessibility;
    }
}