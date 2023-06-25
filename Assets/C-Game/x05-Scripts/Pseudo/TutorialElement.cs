using UnityEngine;

public class TutorialElement : MonoBehaviour
{
    public void CompleteInstruction(bool value)
    {
        gameObject.SetActive(value);
    }
}