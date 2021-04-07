using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
using RPG.Attributes;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 6f;
        [SerializeField] float maxNavPathLength = 40f;    
        
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

        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false; //disable cursor on surfence where player cant move to
            if (GetPathLength(path) > maxNavPathLength) return false;

            return true;
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

        float GetPathLength(NavMeshPath path)
        {
            float total = 0f;
            if (path.corners.Length < 2) return total;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                //sum all distances from corner to another corner
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return total;
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