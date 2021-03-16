using System;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover myMover;

        void Start()
        {
            myMover = GetComponent<Mover>();
        }

        void Update()
        {
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
            else print("nothing to do :|");
        }

        bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(!GetComponent<Fighter>().CanAttack(target)) continue; //przerwya foreach i uznaje resztę "hits" jako true
                
                if (Input.GetMouseButtonDown(1))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true; // zwracamy bool pod if'em ponieważ umożliwia inne opcji interakcji z tą funkcją
            }
            return false;
        }

        bool InteractWithMovement()
        {            
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(1))
                {
                    myMover.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}