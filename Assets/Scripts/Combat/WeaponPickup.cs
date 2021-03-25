using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weaponPickup = null;

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weaponPickup);
                Destroy(gameObject);
            }
        }
    }
}
