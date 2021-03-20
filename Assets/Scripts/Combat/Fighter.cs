using RPG.Movement;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

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
            
            if(target == null) return;
            if(target.IsDead()) return;            

            if (!GetIsInRange())
            {
                myMover.MoveTo(target.transform.position, 1f); //1f full speed
            }
            else
            {
                myMover.Cancel();
                AttackBehaviour();          
            }
        }

        void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }

        void TriggerAttack()
        {
            myAnimator.ResetTrigger("stopAttack");
            myAnimator.SetTrigger("attack"); //this will trigger hit event
        }

        // Animation Event
        void Hit()
        {
            if(target == null) return;
            target.TakeDamage(weaponDamage);
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            myActionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            myMover.Cancel();
        }

        private void StopAttack()
        {
            myAnimator.ResetTrigger("attack");
            myAnimator.SetTrigger("stopAttack");
        }

        bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= weaponRange;
        }
    }
}