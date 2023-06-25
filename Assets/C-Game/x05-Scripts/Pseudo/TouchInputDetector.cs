using UnityEngine;

// RENAME IT TO INPUT DETECTOR INSTEAD
public class TouchInputDetector : MonoBehaviour
{
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); 

            if (touch.phase == TouchPhase.Began)
            {
                HandleTouchInput();
                return;
            } 
        }

        if (Input.GetMouseButtonUp(0))
        {
            HandleMouseInput();
        }
    }

    private void HandleTouchInput()
    {
        TutorialElement tutorialElement = GetComponent<TutorialElement>();

        if (tutorialElement != null)
        {
            tutorialElement.CompleteInstruction(false);
        }
    }

    private void HandleMouseInput()
    {
        TutorialElement tutorialElement = GetComponent<TutorialElement>();

        if (tutorialElement != null)
        {
            tutorialElement.CompleteInstruction(false);
        }
    }
}