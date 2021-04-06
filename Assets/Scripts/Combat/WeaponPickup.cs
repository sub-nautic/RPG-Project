using System.Collections;
using RPG.Attributes;
using RPG.Control;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weaponPickup = null;
        [SerializeField] float healthToRestore = 0f;
        [SerializeField] float respawnTime = 2f;

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Pickup(other.gameObject);
            }
        }

        void Pickup(GameObject subject)
        {
            //if have component with weapon then pick weapon
            if(weaponPickup != null)
            {
                subject.GetComponent<Fighter>().EquipWeapon(weaponPickup);
            }
            if(healthToRestore > 0)
            {
                subject.GetComponent<Health>().Heal(healthToRestore);
            }
            StartCoroutine(HideForSeconds(respawnTime));
        }

        IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
            
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if(Input.GetMouseButtonDown(1))
            {
                Pickup(callingController.gameObject);
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }
}
