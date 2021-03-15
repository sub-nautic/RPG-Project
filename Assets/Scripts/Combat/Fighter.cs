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
            
            if(target == null) return;
            if(target.IsDead()) return;

            if (!GetIsInRange())
            {
                myMover.MoveTo(target.transform.position);
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
                //this will trigger hit event
                myAnimator.SetTrigger("attack");
                timeSinceLastAttack = 0f;                            
            }
        }
        
        // Animation Event
        void Hit()
        {
            target.TakeDamage(weaponDamage);
        }

        public void Attack(CombatTarget combatTarget)
        {
            myActionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            myAnimator.SetTrigger("stopAttack");
            target = null;
        }
        
        bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= weaponRange;
        }
    }
}