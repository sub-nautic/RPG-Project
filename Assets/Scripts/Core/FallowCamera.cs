using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FallowCamera : MonoBehaviour
    {
        [SerializeField] Transform player;

        void Start()
        {

        }

        void LateUpdate() //Late becouse sometimes camera move forward before player character
        {
            transform.position = player.position;
        }
    }
}
