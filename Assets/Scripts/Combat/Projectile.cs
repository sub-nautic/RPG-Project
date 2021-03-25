using System;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Transform target = null;
        [SerializeField] float speed = 1f;

        void Update()
        {
            if(target == null) return;
            
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        Vector3 GetAimLocation()
        {
            //protection to shooting in foots of target
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if(targetCapsule == null)
            {
                return target.position;
            }
            return target.position + Vector3.up * targetCapsule.height / 2;
        }
    }
}