using System;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        float damage = 0f;
        Health target = null;

        bool lookAtTarget = false;

        void Update()
        {
            //In porgress!
            
            if (target == null) return;
            if(!lookAtTarget)
            {
                transform.LookAt(GetAimLocation());
                lookAtTarget = true;
            }
            
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            //Homing();
        }

        void Homing()
        {
            if (target == null) return;

            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        Vector3 GetAimLocation()
        {
            //set where projectile should hit on the target
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if(targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 1.5f;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}