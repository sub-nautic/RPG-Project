using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] float maxSpeed = 6f;        
        
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

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            myActionScheduler.StartAction(this);
            MoveTo(destination, speedFraction);          
        }
        
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            myNav.destination = destination;
            myNav.speed = maxSpeed * Mathf.Clamp01(speedFraction);
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