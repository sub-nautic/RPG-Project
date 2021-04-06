using System.Collections;
using RPG.Control;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weaponPickup = null;
        [SerializeField] float respawnTime = 2f;

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Pickup(other.GetComponent<Fighter>());
            }
        }

        void Pickup(Fighter fighter)
        {
            fighter.EquipWeapon(weaponPickup);
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
                Pickup(callingController.GetComponent<Fighter>());
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }
}
