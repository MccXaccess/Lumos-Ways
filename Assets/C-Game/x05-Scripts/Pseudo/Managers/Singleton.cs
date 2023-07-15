using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            return m_Instance;
        }
    }

    public void Awake()
    {
        m_Instance = (T) (object) this;
    }
}