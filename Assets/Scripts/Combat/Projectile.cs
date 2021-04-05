using System;
using RPG.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = true;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit = null;
        [Tooltip("time to destroy GameObject in hierarchy")]
        [SerializeField] float lifeAfterImpact = 2f;
        [SerializeField] UnityEvent onHit;
        
        float damage = 0f;
        
        Health target = null;
        GameObject instigator = null;

        void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            if (target == null) return;           
            if(isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);                
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.instigator = instigator;
            this.damage = damage;

            Destroy(gameObject, maxLifeTime);
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
            if (target.IsDead()) return;
            
            target.TakeDamage(instigator ,damage);

            speed = 0f;

            onHit.Invoke();
            
            if(hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            //destroy this objects on trigger immediately and then rest after some time
            foreach(GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeAfterImpact);
        }        
    }
}