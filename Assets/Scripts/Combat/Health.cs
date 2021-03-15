using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;

        public void TakeDamage(float damage)
        {
            // jeżeli wartość damage jest wyższa niż health zwraca 0f
            health = Mathf.Max(health - damage, 0f);
            print(health);           
        }
    }
}