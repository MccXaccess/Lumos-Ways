using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;

    private int sceneIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    public void ChangeSceneTo(string value)
    {
        SceneManager.LoadScene(value);
    }

    private void NextSceneFixed()
    {
        sceneIndex++;

        if (sceneIndex > SceneManager.sceneCountInBuildSettings)
        {
            sceneIndex = 0;
        }

        SceneManager.LoadScene(sceneIndex);
    }

    private void NextSceneRelative()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}