using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;

    private int sceneIndex;
    private int m_CurrentScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
        // else if (Instance != this)
        // {
        //     Destroy(gameObject);
        // }
    }

    public void OnSceneExit()
    {
        Time.timeScale = 1.0f;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }
    
    public void ChangeSceneTo(string value)
    {
        SceneManager.LoadScene(value);
    }

    public void NextSceneFixed()
    {
        sceneIndex++;

        if (sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            sceneIndex = 0;
        }

        SceneManager.LoadScene(sceneIndex);
    }

    public void NextSceneRelative()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public int GetRelativeSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex + 1;
    }
}