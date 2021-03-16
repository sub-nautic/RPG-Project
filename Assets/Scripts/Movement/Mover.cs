using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent myNav;
        Animator myAnimator;
        ActionScheduler myActionScheduler;
        Health health;

        void Start()
        {
            myNav = GetComponent<NavMeshAgent>();
            myAnimator = GetComponent<Animator>();
            myActionScheduler = GetComponent<ActionScheduler>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            myNav.enabled = !health.IsDead();
            
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            myActionScheduler.StartAction(this);
            MoveTo(destination);          
        }
        
        public void MoveTo(Vector3 destination)
        {
            myNav.destination = destination;
            myNav.isStopped = false;
        }

        public void Cancel()
        {
            myNav.isStopped = true;
        }

        void UpdateAnimator()
        {
            //have to change global velocity to local velocity
            Vector3 velocity = myNav.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            float speed = localVelocity.z;
            myAnimator.SetFloat("forwardSpeed", speed);
        }
    }
}