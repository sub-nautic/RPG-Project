using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }
        
        void DisableControl(PlayableDirector play)
        {
            print("DisableControl");
        }

        void EnableControl(PlayableDirector stop)
        {
            print("EnableControl");
        }
    }
}