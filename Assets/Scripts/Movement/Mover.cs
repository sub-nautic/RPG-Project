using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
using RPG.Resources;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 6f;        
        
        NavMeshAgent myNav;
        Animator myAnimator;
        ActionScheduler myActionScheduler;
        Health health;

        void Awake()
        {
            myNav = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
            myAnimator = GetComponent<Animator>();
            myActionScheduler = GetComponent<ActionScheduler>();
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

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            //casting method becouse you shouldn't use (object state) as SerializableVector3 in this function
            SerializableVector3 position = (SerializableVector3)state;

            GetComponent<NavMeshAgent>().enabled = false; //if you use nav mesh agent, this protect from some errors
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}