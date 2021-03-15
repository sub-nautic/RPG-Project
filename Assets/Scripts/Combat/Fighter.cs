using RPG.Movement;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        
        Transform target;
        float timeSinceLastAttack = 0f;

        Mover myMover;
        ActionScheduler myActionScheduler;

        Animator myAnimator;
        
        void Start()
        {
            myMover = GetComponent<Mover>();
            myActionScheduler = GetComponent<ActionScheduler>();
            myAnimator = GetComponent<Animator>();
        }
        
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            
            if(target == null) { return; }

            if (target != null && !GetIsInRange())
            {
                myMover.MoveTo(target.position);
            }
            else
            {
                myMover.Cancel();
                AttackBehaviour();                           
            }
        }

        void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                myAnimator.SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            myActionScheduler.StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
        
        bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) <= weaponRange;
        }

        // Animation Event
        void Hit()
        {
            
        }
    }
}