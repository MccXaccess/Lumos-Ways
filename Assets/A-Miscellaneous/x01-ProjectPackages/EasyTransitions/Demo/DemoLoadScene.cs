using UnityEngine;

namespace EasyTransition
{

    public class DemoLoadScene : MonoBehaviour
    {
        public TransitionSettings transition;
        public float startDelay;

        
        public void LoadScene(string _sceneName)
        {
            TransitionManager.Instance().Transition(_sceneName, transition, startDelay);
        }

        public void LoadScene(int _sceneName)
        {
            TransitionManager.Instance().Transition(_sceneName, transition, startDelay);
        }   

        public void StartTransition()
        {
            TransitionManager.Instance().Transition(transition, startDelay);
        }
    }
}